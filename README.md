# PS4 PKG Tool
![image](https://user-images.githubusercontent.com/36906814/87872280-9d30b780-c9e9-11ea-871e-c8514132394b.png)

This tool allows us to display PS4 PKG library, manage and perform various operations on PS4 PKG.
I re-wrote many parts of this tool and changed the program versioning because the previous versioning is messed up. For new release I will start with v1.0. Some features is disabled as it not working properly and listed it in todo checklist. All suggestion are welcome. Report if there any bugs [here](https://github.com/pearlxcore/PS4-PKG-Tool/issues).

**This is not a software which allows you to get free PS4 game.**

# Requirement
- [.Net Framework 4.6](https://www.microsoft.com/en-nz/download/details.aspx?id=48130)

# Features
- Scan and view your collection of PS4 PKG library.
- View pkg info such PKG param info, PKG trophy list and PKG entry.
- Rename and export PKG to excel file.
- Check PKG update.

# How to use Remote Package Installer feature

This only compatible with any PS4 firmware capable running Flatz's Remote Package Installer app. Splitted update PKG is not supported at this moment.

- Open program settings.
- Set PC and PS4 IP address.
- Install node.js and http-server module. (Make sure Node.js allowed through firewall)
- If you unable to install http-server via PS4 PKG Tool, restart PS4 PKG Tool and try reinstall the module or you can install it manually using command 'npm install http-server -g' on command prompt.
- Save and Exit program settings.
- Launch Remote Package Installer apps on your PS4.
- Select PKG you wish to install, right-click and select 'Send PKG to PS4'.

# Todo list
- [ ] Add DLC unlocker
- [ ] Add DLC checker
- [ ] Add PKG update checker
- [ ] Add PKG header info
- [ ] Add extract PKG
- [ ] Add PKG hashes and signatures
- [ ] Add feature to segregate PKG based on Category (game/patch/addon)
- [x] 'Hardisk Free Space' now displaying every available/connected hardisk partition
- [x] Add new program setting UI
- [x] Add pkg sender to send PKG to PS4 via network (WIP)
- [x] Add option to delete PKG
- [x] Add option for recursive folder scan
- [x] Add feature to set pkg image as desktop background image
- [x] Add new PKG renaming format to prevent renaming error : Cannot create a file when the file already exists
- [x] Fix issue https://github.com/pearlxcore/PS4-PKG-Tool/issues/6
- [x] Fix issue https://github.com/pearlxcore/PS4-PKG-Tool/issues/7
- [x] Fix "Illegal characters path" error while renaming PKG
- [x] Add new PKG renaming format 

# Support my work with Ko-Fi
https://ko-fi.com/pearlxcore

# Credit
- [xXxTheDarkprogramerxXx](https://github.com/xXxTheDarkprogramerxXx) for the [PS4 Tool](https://github.com/xXxTheDarkprogramerxXx/PS4_Tools) lib
- [Maxton](https://github.com/maxton) for [LibOrbisPkg](https://github.com/maxton/LibOrbisPkg)
- [stooged](https://github.com/stooged) for [psDLC](https://github.com/stooged/psDLC)
- Sony <3
