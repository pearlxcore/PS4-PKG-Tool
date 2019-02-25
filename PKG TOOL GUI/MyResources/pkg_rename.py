## pkg_rename by n1ghty
REL_VERSION = 'v1.1.2'

import sys, os, struct, traceback, re, codecs, argparse
from lib import pkg_parser, common

sys.stdout = codecs.getwriter('utf8')(sys.stdout)
sys.stderr = codecs.getwriter('utf8')(sys.stderr)

NAME_FORMAT = u'%TITLE% (%TITLE_ID%) [v%VER%]'
FORMAT_1 = u'%TITLE%'
FORMAT_2 = u'%TITLE_ID%'
FORMAT_3 = u'%CONTENT_ID%'
FORMAT_4 = u'%TITLE% (%TITLE_ID%)'
FORMAT_5 = u'%TITLE% (%REGION%)'
FORMAT_6 = u'%TITLE% (%TITLE_ID%) [%REGION%]'
## parse arguments
parser = argparse.ArgumentParser(
	description = 'This tool renames PS4 pkg files to the sony format (default), a readable\n'
											'name format or a custom specified format.\n'
											'\n'
											'For the custom formatting, values can be replaced by surrounding them with\n'
											'%-characters.\n'
											'E.g. \'%TITLE% (%TITLE_ID%)\' will result in \'Game name (CUSA01234)\'\n'
											'\n'
											'Available values for formatting:\n'
											+ pkg_parser.AVAILABLE_VALUES +
											'\n'
											'The readable name format (-n) uses the following format:\n'
											'\'' + NAME_FORMAT + '\'',
	formatter_class=common.Formatter
	)
parser.add_argument('pkg_path', type=unicode, help='the pkg file which shall be renamed (or directory when used with -d)')
parser.add_argument('-t', dest='testrun', action='store_true', help='only test the formatting without renaming')
parser.add_argument('-c', dest='custom_format', type=unicode, help='custom file name format')
parser.add_argument('-n', dest='name_format', action='store_true', help='use a readable name format')
parser.add_argument('-d', dest='dir', action='store_true', help='rename all files in the specified directory')
parser.add_argument('-r', dest='recursive', action='store_true', help='include subdirectories')
parser.add_argument('-1', dest='format_1', action='store_true', help='use a readable name format')
parser.add_argument('-2', dest='format_2', action='store_true', help='use a readable name format')
parser.add_argument('-3', dest='format_3', action='store_true', help='use a readable name format')
parser.add_argument('-4', dest='format_4', action='store_true', help='use a readable name format')
parser.add_argument('-5', dest='format_5', action='store_true', help='use a readable name format')
parser.add_argument('-6', dest='format_6', action='store_true', help='use a readable name format')

try:
	args = parser.parse_args()
except:
	print

	sys.exit()

if (args.dir):
	pkg_path = args.pkg_path
	if os.path.isfile(pkg_path):
		print ("Error: invalid directory specified")
		sys.exit()
else:
	if not os.path.isfile(args.pkg_path):
		print ("error: invalid file specified")
		sys.exit()

## utility functions
def getReadableString(s):
	try:
		s_u = s.decode('utf-8')
	except:
		s_u = s
	return s_u

def doDictFormat(s, dict):
	s_f = s
	format_val_arr = []
	format_arr = re.findall('\%(.*?)\%', s)
	for val in format_arr:
		if (val.upper().startswith('TITLE') and val.upper() != 'TITLE_ID'):
			title = getReadableString(dict[val.upper()])
			# replace invalid characters for filenames
			title = title.replace(': ', ' - ').replace(':','-').replace('|','l').replace('?','')
			title = title.replace('/','_').replace('\\','_').replace('*','_')
			title = title.replace('<','(').replace('>',')')
			# replacing of reserved names like 'nul', 'com1' etc is probably not needed
			s_f = s_f.replace('%' + val + '%', '{}')
			format_val_arr.append(title)
		elif val.upper() in dict:
			s_f = s_f.replace('%' + val + '%', '{}')
			format_val_arr.append(dict[val.upper()])
	return s_f.format(*format_val_arr)

## main code
def renamePkg(pkg_file_path):
	try:
		pkgInfo = pkg_parser.getPkgInfo(pkg_file_path)

		if (pkgInfo):
			format_out = ''
			if (args.custom_format):
				# use custom formatting
				format_out = doDictFormat(args.custom_format, pkgInfo)
			elif (args.format_1):
				# format with readable name
				format_out = doDictFormat(FORMAT_1, pkgInfo)
			elif (args.format_2):
				# format with readable name
				format_out = doDictFormat(FORMAT_2, pkgInfo)
			elif (args.format_3):
				# format with readable name
				format_out = doDictFormat(FORMAT_3, pkgInfo)
			elif (args.format_4):
				# format with readable name
				format_out = doDictFormat(FORMAT_4, pkgInfo)
			elif (args.format_5):
				# format with readable name
				format_out = doDictFormat(FORMAT_5, pkgInfo)
			elif (args.format_6):
				# format with readable name
				format_out = doDictFormat(FORMAT_6, pkgInfo)
			elif (args.name_format):
				# format with readable name
				format_out = doDictFormat(NAME_FORMAT, pkgInfo)
			else:
				# use default sony format
				if pkgInfo['CONTENT_ID'] and pkgInfo['APP_VER'] and pkgInfo['VERSION']:
					format_out = '{0}-A{1}-V{2}'.format(pkgInfo['CONTENT_ID'], pkgInfo['APP_VER'].replace('.',''), pkgInfo['VERSION'].replace('.',''))
				else:
					raise pkg_parser.MyError('parsing of param.sfo failed')
			format_out = format_out + '.pkg'

			print ('> Renaming to ') + format_out
			if (args.testrun == False):
				if (os.path.split(pkg_file_path)[1] == format_out):
					print ("> Filename already exists.")
				else:
					pkg_new_file_path = os.path.dirname(os.path.abspath(pkg_file_path)) + '\\' + format_out
					if os.path.exists(pkg_new_file_path):
						raise pkg_parser.MyError('file \''+pkg_new_file_path+'\' already exists.')
					else:
						os.rename(pkg_file_path, pkg_new_file_path)
						print ('> Done.')

	except pkg_parser.MyError as e:
		print ('ERROR:'), e.message
	except:
		print ('ERROR: unexpected error:  {} ({})').format(sys.exc_info()[0], pkg_file_path)
		traceback.print_exc(file=sys.stdout)

if (args.dir):
	for root, directories, files in os.walk(pkg_path):
		for file in files: 
			if file.lower().endswith('.pkg'):
				renamePkg(os.path.join(root, file))
		if not (args.recursive):
			break
else:
	renamePkg(args.pkg_path)
