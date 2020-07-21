# PS4 PKG Tool
![image](https://user-images.githubusercontent.com/36906814/87872280-9d30b780-c9e9-11ea-871e-c8514132394b.png)

This tool allows us to display PS4 PKG library, manage and perform various operations on PS4 PKG.
I re-wrote many parts of this tool and changed the program versioning because the previous versioning is messed up. For new release I will start with v1.0. Some features is disabled as it not working properly and listed it in todo checklist. All suggestion are welcome. Report if there any bugs [here](https://github.com/pearlxcore/PS4-PKG-Tool/issues).

**This is not a software which allows you to get free PS4 game.**

# Features
- Scan and view your collection of PS4 PKG library.
- View pkg info such PKG param info, PKG trophy list and PKG entry.
- Rename and export PKG to excel file.
- Check PKG update.

# Todo list
- [ ] Add DLC checker
- [ ] Add PKG update checker
- [ ] Add PKG backport tool
- [ ] Add PKG header info
- [ ] Add extract PKG
- [ ] Add PKG hashes and signatures
- [ ] Add feature to segregate PKG based on Category (game/patch/addon)
- [ ] Add PS2 Fake PKG Generator
- [ ] Add PS1 Fake PKG Generator
- [x] Add feature to set pkg image as desktop background image
- [x] Add new PKG renaming format to prevent renaming error : [TITLE_ID] [CATEGORY] [VERSION] TITLE, TITLE [CATEGORY] [VERSION]. Renaming game and its update PKG in the same directory will throw an exception : Cannot create a file when the file already exists.

# Credit
- [xXxTheDarkprogramerxXx](https://github.com/xXxTheDarkprogramerxXx) for the [PS4 Tool](https://github.com/xXxTheDarkprogramerxXx/PS4_Tools) lib
- [Maxton](https://github.com/maxton) for [LibOrbisPkg](https://github.com/maxton/LibOrbisPkg)
- [stooged](https://github.com/stooged) for [psDLC](https://github.com/stooged/psDLC)
- [DefaultDNB](https://twitter.com/DefaultDNB) for testing
- Sony <3
