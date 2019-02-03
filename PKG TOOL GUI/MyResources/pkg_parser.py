## pkg_parser lib by n1ghty

## This file is based on
## UnPKG rev 0x00000008 (public edition), (c) flatz
## and
## Python SFO Parser by: Chris Kreager a.k.a LanThief

import sys, os, struct, traceback

# text of available values for help texts
AVAILABLE_VALUES = (
	' Raw values from param.sfo like\n'
	'  - TITLE, TITLE_ID, CONTENT_ID, VERSION, APP_VER, PARENTAL_LEVEL, \n'
	'    SYSTEM_VER, ...\n'
	' Formatted values, especially for version information:\n'
	'  - LANGUAGES\n'
	'    The list of title name languages, e.g. \'EN,FR,RU\'\n'
	'    This does not always reflect supported languages.'
	'  - VER\n'
	'    Equals VERSION for a game / an application and APP_VER(U) for an update\n'
	'  - SYS_VER\n'
	'    The required system version number in a readable format, e.g. \'2.70\'\n'
	'  - SDK_VER\n'
	'    The used sdk version number in a readable format - if available - e.g. \'2.70\'\n'
	'  - REGION\n'
	'    The region of the pkg (CN, EU, US)\n'
	'  - SIZE\n'
	'    The filesize in a readable format, e.g. \'1.1 GB\'\n'
	'  - TITLE_XX\n'
	'    The title name in a specific language XX. If not available, the default\n'
	'    language is used.\n'
	'\n'
	'    Available language codes:\n'
	'      JA, EN, FR, ES, DE, IT, NL, PT, RU, KO, CH, ZH, FI, SV, DA,\n'
	'      NO, PL, BR, GB, TR, LA, AR, CA, CS, HU, EL, RO, TH, VI, IN'
	)

## utility functions
def convert_bytes(num):
	"this function will convert bytes to MB.... GB... etc"
	for x in ['bytes', 'KB', 'MB', 'GB', 'TB']:
		if num < 1024.0:
			return '%3.1f %s' % (num, x)
		num /= 1024.0

def read_string(f, length):
	return f.read(length)
def read_cstring(f):
	s = ''
	while True:
		c = f.read(1)
		if not c:
			return False
		if ord(c) == 0:
			break
		s += c
	return s

def read_uint32_be(f):
	return struct.unpack('>I', f.read(struct.calcsize('>I')))[0]

def str2hex(s, size=8):
	"String converter to hex"
	if (len(s) * size) <= 32:
		h = 0x0
	else:
		h = 0x0L
	for c in s:
		h = (h << size) | ord(c)
	return h

def le32(bits):
	result = 0x0
	offset = 0
	for i in xrange(4):
		byte = ord(bits[i])
		result |= byte << offset
		offset += 8
	return result

def le16(bits):
	return (ord(bits[0]) | ord(bits[1]) << 8)

## classes
class PsfHdr:
	size = 20

	def __init__(self, bits):
		self.size = 20
		self.data = bits[:self.size]
		self.magic = le32(bits[:4])
		self.rfu000 = le32(bits[4:8])
		self.label_ptr = bits[8:12]
		self.data_ptr = bits[12:16]
		self.nsects = bits[16:20]

	def __len__(self):
		return self.size


class PsfSec:
	size = 16

	def __init__(self, bits):
		self.size = 16
		self.data = bits[:self.size]
		self.label_off = bits[:2]
		self.rfu001 = bits[2:3]
		self.data_type = str2hex(bits[3:4])  # string=2, integer=4, binary=0
		self.datafield_used = bits[4:8]
		self.datafield_size = bits[8:12]
		self.data_off = bits[12:16]

	def __len__(self):
		return self.size


class MyError(Exception):
	def __init__(self, message):
		self.message = message

	def __str__(self):
		return repr(self.message)

class FileTableEntry:
	entry_fmt = '>IIIIII8x'

	def __init__(self):
		pass

	def read(self, f):
		self.type, self.unk1, self.flags1, self.flags2, self.offset, self.size = struct.unpack(self.entry_fmt, f.read(struct.calcsize(self.entry_fmt)))
		self.key_index = (self.flags2 & 0xF000) >> 12
		self.name = None

## main code
PsfMagic = '\0PSF'
PkgMagic = '\x7FCNT'
TITLE_LANG_MAP = {
			'00' : 'JA', '01' : 'EN', '02' : 'FR', '03' : 'ES', '04' : 'DE',
			'05' : 'IT', '06' : 'NL', '07' : 'PT', '08' : 'RU', '09' : 'KO',
			'10' : 'CH', '11' : 'ZH', '12' : 'FI', '13' : 'SV', '14' : 'DA',
			'15' : 'NO', '16' : 'PL', '17' : 'BR', '18' : 'GB', '19' : 'TR',
			'20' : 'LA', '21' : 'AR', '22' : 'CA', '23' : 'CS', '24' : 'HU',
			'25' : 'EL', '26' : 'RO', '27' : 'TH', '28' : 'VI', '29' : 'IN',
				}

def getPkgInfo(pkg_file_path):
	try:
		with open(pkg_file_path, 'rb') as pkg_file:
			magic = read_string(pkg_file, 4)
			if magic != PkgMagic:
				raise MyError('invalid file magic')

			pkg_file.seek(0x10)
			num_table_entries = read_uint32_be(pkg_file)

			pkg_file.seek(0x18)
			file_table_offset = read_uint32_be(pkg_file)

			table_entries = []
			table_entries_map = {}
			pkg_file.seek(file_table_offset)
			for i in xrange(num_table_entries):
				entry = FileTableEntry()
				entry.read(pkg_file)
				table_entries_map[entry.type] = len(table_entries)
				table_entries.append(entry)

			for i in xrange(num_table_entries):
				entry = table_entries[i]
				if entry.type == 0x1000:
					pkg_file.seek(entry.offset)
					data = pkg_file.read(entry.size)
					if not data.find(PsfMagic) == 0:
						raise MyError('param.sfo is not a PSF file ! [PSF Magic == 0x%08X]\n' % str2hex(PsfMagic))

					psfheader = PsfHdr(data)
					psfsections = PsfSec(data[PsfHdr.size:])
					psflabels = data[le32(psfheader.label_ptr):]
					psfdata = data[le32(psfheader.data_ptr):]

					index = PsfHdr.size
					sect = psfsections

					# parse param.sfo info
					pkg_info = {}
					for i in xrange(0, le32(psfheader.nsects)):
						val_label = psflabels[le16(sect.label_off):].split('\x00')[0]
						#data_types: string=2, integer=4, binary=0
						val_data = ''
						if (sect.data_type == 2):
							val_data = psfdata[le32(sect.data_off):le32(sect.data_off)+le32(sect.datafield_used)-1]
							pkg_info[val_label] = val_data
						elif (sect.data_type == 4):
							val_data = psfdata[le32(sect.data_off):le32(sect.data_off)+le32(sect.datafield_used)]
							val_data = '%X' % le32(val_data)
							pkg_info[val_label] = val_data

						index += PsfSec.size
						sect = PsfSec(data[index:])

			# additional infos

			# get filesize
			pkg_file.seek(0, os.SEEK_END)
			pkg_info['SIZE'] = convert_bytes(pkg_file.tell())

			# get region
			if (pkg_info['CONTENT_ID'][0] == 'E'):
				region = 'EU'
			elif (pkg_info['CONTENT_ID'][0] == 'U'):
				region = 'US'
			elif (pkg_info['CONTENT_ID'][0] == 'H'):
				region = 'CN'
			else:
				region = 'UNKNOWN'
			pkg_info['REGION'] = region

			# readable system version number
			if ('SYSTEM_VER' in pkg_info):
				pkg_info['SYS_VER'] = '{}.{}'.format(pkg_info['SYSTEM_VER'][0], pkg_info['SYSTEM_VER'][1:3])
			
			# readable sdk version number
			if ('PUBTOOLINFO' in pkg_info):
				for ptinfo in pkg_info['PUBTOOLINFO'].split(','):
					var = ptinfo.split('=')[0]
					val = ptinfo.split('=')[1]
					if (var == 'sdk_ver'):
						pkg_info['SDK_VER'] =  '{}.{}'.format(val[1], val[2:4])

			# title names
			for k, v in TITLE_LANG_MAP.iteritems():
				var = 'TITLE_' + k
				var_l = 'TITLE_' + v
				if (var in pkg_info):
					pkg_info[var_l] = pkg_info[var]
				else:
					pkg_info[var_l] = pkg_info['TITLE']

			# languages
			languages = []
			for k, v in TITLE_LANG_MAP.iteritems():
				var = 'TITLE_' + k
				if (var in pkg_info):
					if not (pkg_info[var] == ''):
						languages.append(v)
			pkg_info['LANGUAGES'] = ','.join(languages)

			# add combined version for update / game versions
			if (pkg_info['CATEGORY'] == 'gp'):
				# update, replace version
				pkg_info['VER'] = pkg_info['APP_VER'] + '(U)'
			else:
				pkg_info['VER'] = pkg_info['VERSION']

			pkg_file.close()
			return pkg_info

	except IOError:
		print u'ERROR: i/o error during processing ({})'.format(pkg_file_path)
	except MyError as e:
		print u'ERROR: {} ({})'.format(e.message, pkg_file_path)
	except:
		print u'ERROR: unexpected error:  {} ({})'.format(sys.exc_info()[0], pkg_file_path)
		traceback.print_exc(file=sys.stdout)
