## pkg_list by n1ghty

import sys, os, struct, traceback, argparse
from lib import pkg_parser, xlsxlist, common



## parse arguments
parser = argparse.ArgumentParser(
	description = 'This tool parses all pkg files in the specified directory/directories recursively\n'
											'and generates an excel sheet from the parsed infos.\n'
											'\n'
											'Available values for the columns:\n'
											+ pkg_parser.AVAILABLE_VALUES,
	formatter_class=common.Formatter
	)
parser.add_argument('pkg_path', type=unicode, nargs='+', help='the path(s) to scan for pkg files')
parser.add_argument('-r', dest='recursive', action='store_true', help='include subdirectories')
parser.add_argument('-c', dest='column', nargs='+', help='specify the columns')
parser.add_argument('-s', dest='sort', help='sort list by specific column')
parser.add_argument('-d', dest='descending', action='store_true', help='use descending sorting')
parser.add_argument('-o', dest='outfile', type=unicode, help='specify the output file name (without suffix)')

parser.set_defaults(column=['TITLE', 'TITLE_ID', 'REGION', 'VER', 'CONTENT_ID', 'SIZE'], outfile='PKG_List')



        
try:
	args = parser.parse_args()
except:
	print
	print "See help (-h) for commands"
	sys.exit()

# arg cleanup
if (args.sort):
	args.sort = args.sort.upper()
args.column = map(str.upper, args.column)

pkg_paths = []
for path in args.pkg_path:
	if not os.path.isdir(path):
		print 'ERROR: invalid path specified'
		sys.exit()
	pkg_paths.append(os.path.abspath(path))

if len(args.column) != len(set(args.column)):
	print 'ERROR: duplicate values in column list'
	sys.exit()

## utility functions
def getReadableString(s):
	try:
		s_u = s.decode('utf-8')
	except:
		s_u = s
	return s_u

## main code
# parse files


        
pkgInfos = {'app' : [], 'upd' : [], 'ps2' : [], 'err' : [], 'count' : 0}
for pkg_path in pkg_paths:
	for root, directories, files in os.walk(pkg_path):
		for file in files: 
			if file.lower().endswith('.pkg'):
				try:
					pkgInfos['count'] += 1
					pkgInfo = pkg_parser.getPkgInfo(os.path.join(root, file))
					# set worksheet
					if (pkgInfo['CATEGORY'] == 'gp'):
						# update
						pkgInfos['upd'].append(pkgInfo)
					elif (pkgInfo['CATEGORY'] == 'gpo'):
						# PS2
						pkgInfos['ps2'].append(pkgInfo)
					else:
						# Application / Game 'gd' and unknown
						pkgInfos['app'].append(pkgInfo)
				except:
					# failed to parse pkg
					pkgInfos['err'].append(file)
		if not (args.recursive):
			break

print 'Found {} files:\n'.format(pkgInfos['count'])
print '{} PKG Applications/Games'.format(len(pkgInfos['app']))
print '{} PKG Updates'.format(len(pkgInfos['upd']))
print '{} PKG PS2 Games'.format(len(pkgInfos['ps2']))
print '{} PKG files failed to parse'.format(len(pkgInfos['err']))
print

# sort lists
if args.sort and (args.sort in args.column):
	for id in ('app', 'upd', 'ps2'):
		# add value to every info to make it sortable
		for info in pkgInfos[id]:
			if not (args.sort in info):
				info[args.sort] = ''
		pkgInfos[id] = sorted(pkgInfos[id], key=lambda k: k[args.sort], reverse=args.descending)

# sort errorlist
pkgInfos['err'] = sorted(pkgInfos['err'])

# generate xlsx list
xlsxlist.writeFile(pkgInfos, args.column, args.outfile)
