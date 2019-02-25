import sys, os, struct, traceback, xlsxwriter

DEFAULT_STYLE = 'Table Style Medium 8'
DEFAULT_SIZES = {'TITLE' : 62, 'TITLE_ID' : 10, 'REGION' : 9, 'VERSION' : 10, 'APP_VER' : 10, 'CONTENT_ID' : 42, 'SIZE' : 10, 'SYS_VER' : 10, 'SDK_VER' : 10, 'LANGUAGES' : 58, 'VER' : 8}
UNKNOWN_SIZE = 14

## utility functions
def getReadableString(s):
	try:
		s_u = s.decode('utf-8')
	except:
		s_u = s
	return s_u

## main code

        
def writeRow(pkgInfo, sheet, row, fieldnames):
	# fill row
	for pos in range(len(fieldnames)):
		if fieldnames[pos] in pkgInfo:
			if (fieldnames[pos].startswith('TITLE') and not fieldnames[pos] == 'TITLE_ID'):
				sheet.write(row, pos, getReadableString(pkgInfo[fieldnames[pos]]))
			else:
				sheet.write(row, pos, pkgInfo[fieldnames[pos]])

def writeSheet(wb, id, title, pkgInfos, fieldnames):
	if (len(pkgInfos[id]) > 0):
		sheet = wb.add_worksheet(title)
		for i in range(len(pkgInfos[id])):
			writeRow(pkgInfos[id][i], sheet, i + 1, fieldnames)
		return sheet
	return None

def writeFile(pkgInfos, fieldnames, filename):
	list_file = filename + '.xlsx'
	workbook = xlsxwriter.Workbook(list_file)
	worksheets = {}

	worksheets['app'] = writeSheet(workbook, 'app', 'Applications', pkgInfos, fieldnames)
	worksheets['upd'] = writeSheet(workbook, 'upd', 'Updates', pkgInfos, fieldnames)
	worksheets['ps2'] = writeSheet(workbook, 'ps2', 'PS2', pkgInfos, fieldnames)

	if (('err' in pkgInfos) and (len(pkgInfos['err']) > 0)):
		worksheets['err'] = workbook.add_worksheet('Failures')
		for i in range(len(pkgInfos['err'])):
			worksheets['err'].write(i + 1, 0, pkgInfos['err'][i])
	else:
		worksheets['err'] = None

	# prepare header
	header = []
	for pos in range(len(fieldnames)):
		header.append({'header': fieldnames[pos]})
	
	if (worksheets['app']):
		worksheets['app'].add_table(0, 0, len(pkgInfos['app']), len(fieldnames)-1, {'style': DEFAULT_STYLE, 'columns' : header})
	if (worksheets['upd']):
		worksheets['upd'].add_table(0, 0, len(pkgInfos['upd']), len(fieldnames)-1, {'style': DEFAULT_STYLE, 'columns' : header})
	if (worksheets['ps2']):
		worksheets['ps2'].add_table(0, 0, len(pkgInfos['ps2']), len(fieldnames)-1, {'style': DEFAULT_STYLE, 'columns' : header})
	if (worksheets['err']):
		worksheets['err'].add_table(0, 0, len(pkgInfos['err']), 0, {'style': DEFAULT_STYLE, 'columns' : [{'header' : 'Filename'}]})
		worksheets['err'].set_column(0, 0, 120)  # filename width

	# set column sizes
	for id in ('app', 'upd', 'ps2'):
		if (worksheets[id]):
			for i in range(len(fieldnames)):
				fieldname = fieldnames[i]
				if fieldname in DEFAULT_SIZES:
					size = DEFAULT_SIZES[fieldname]
				else:
					if fieldname.startswith('TITLE'):
						size = DEFAULT_SIZES['TITLE']
					else:
						size = UNKNOWN_SIZE
				worksheets[id].set_column(i, i, size)

	try:
		workbook.close()
	except:
		print 'ERROR: unable to write to file', list_file
		sys.exit()

	print 'Saved xlsx list to {}'.format(list_file)
