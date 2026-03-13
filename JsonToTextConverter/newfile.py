import json
import os
import re
import sys
#multifile = boolean, exportfile = string of export file location
multifile = sys.argv[1:][0]
exportfile = sys.argv[1:][1]
#handling the export json
with open(exportfile) as file:
	theexport=file.read()
string = json.loads(theexport)
#make folder for all of these to go into instead of being unleashed on the world
os.mkdir("%s"%string["users"][0]["username"] +"SimplyPluralBackup")
for i in string["members"]:
	thealter = {}
	#transfer alter from json to where i can mess with it safely
	for x in i:
		thealter[x] = i[x]
	#rename all custom fields to their proper names
	for j in string["customFields"]:
		if "info" in thealter:
			thealter["info"][j["name"]] = thealter["info"].pop(j["_id"],"")
	thealter["notes"] = []
	#find notes owned by alter
	for k in string["notes"]:
		if k["member"] == thealter["_id"]:
			#popping hard to read/irrelevant things
			k.pop('supportMarkdown','')
			k.pop('_id','')
			k.pop('date','')
			k.pop('uid','')
			k.pop('lastOperationTime','')
			#popping broke for this one for some reaaon
			k["member"] = ''
			#add to list
			thealter["notes"].append(k)
	#popping hard to read/irrelevant things
	thealter.pop("_id",'')
	thealter.pop("preventTrusted",'')
	thealter.pop("receiveMessageBoardNotifs",'')
	thealter.pop('pkId','')
	thealter.pop('private','')
	thealter.pop('frame','')
	thealter.pop('buckets','')
	thealter.pop('archived','')
	thealter.pop('preventsFrontNotifs','')
	thealter.pop('archivedReason','')
	thealter.pop('uid','')
	thealter.pop('supportDescMarkdown','')
	thealter.pop('lastOperationTime','')
	#if they want 3 files per alter
	if multifile == True:
		with open("%s"%string["users"][0]['username']+"SimplyPluralBackup/%s"%re.sub(r'[^\w\s]', '-', thealter['name'])+"Base.txt","w") as newfile:
			newfile.write('Name\n%s'%thealter['name']+'\nDescription\n%s'%thealter['desc'])
		if 'info' in thealter:
			with open("%s"%string["users"][0]['username']+"SimplyPluralBackup/%s"%re.sub(r'[^\w\s]', '-', thealter['name'])+"CustomFields.txt","w") as newfiletwo:
				for key, value in thealter['info'].items():
					newfiletwo.write('%s'%key+'\n%s'%value+'\n\n')
		with open("%s"%string["users"][0]['username']+"SimplyPluralBackup/%s"%re.sub(r'[^\w\s]', '-', thealter['name'])+"Notes.txt",'w') as newfilethree:
			for note in thealter['notes']:
				newfilethree.write('Title:%s'%note['title']+'\n%s'%note['note']+'\nColor:%s'%note['color']+'\n\n')
	#if they dont
	else:
		with open("%s"%string["users"][0]['username']+"SimplyPluralBackup/%s"%re.sub(r'[^\w\s]', '-', thealter['name'])+"FullBackup.txt",'w') as newfilefull:
			newfilefull.write('Name\n%s'%thealter['name']+'\nDescription\n%s'%thealter['desc']+'\n\n\n')
			if 'info' in thealter:
				for key, value in thealter['info'].items():
					newfilefull.write('%s'%key+'\n%s'%value+'\n\n')
				newfilefull.write('\n')
			for note in thealter['notes']:
				newfilefull.write('Title:%s'%note['title']+'\n%s'%note['note']+'\nColor:%s'%note['color']+'\n\n')
