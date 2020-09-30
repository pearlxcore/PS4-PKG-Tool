# PS4 PKG Tool
![image](https://user-images.githubusercontent.com/36906814/94719448-bf4d9b00-0385-11eb-812d-b8ce5b89416f.png)

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
- Send PKG to PS4 via network.

# How to use Remote Package Installer feature
This only compatible with any PS4 firmware capable running Flatz's Remote Package Installer app. Splitted update PKG is not supported at this moment.

- Open program settings.
- Set PC and PS4 IP address.
- Install node.js and http-server module. (Make sure Node.js allowed through firewall)
- If you unable to install http-server via PS4 PKG Tool, restart PS4 PKG Tool and try reinstall the module or you can install it manually using command 'npm install http-server -g' on command prompt.
- Save and Exit program settings.
- Launch Remote Package Installer apps on your PS4.
- Select PKG you wish to install, right-click and select 'Send PKG to PS4'.

# Download
https://github.com/pearlxcore/PS4-PKG-Tool/releases

# Support my work with Ko-Fi
[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/R6R524N7X)

# Credit
- [xXxTheDarkprogramerxXx](https://github.com/xXxTheDarkprogramerxXx) for the [PS4 Tool](https://github.com/xXxTheDarkprogramerxXx/PS4_Tools) lib
- [Maxton](https://github.com/maxton) for [LibOrbisPkg](https://github.com/maxton/LibOrbisPkg)
- [stooged](https://github.com/stooged) for [psDLC](https://github.com/stooged/psDLC)
- Sony <3
