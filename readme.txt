------------------------------------------------------------------------------
CABDevExpress.ExtensionKit 3
http://www.codeplex.com/CABDevExpress
------------------------------------------------------------------------------

This is the first release of the CAB DevExpress Extension Kit on CodePlex but
because of the history before CodePlex, where 2 releases were made, is known 
as Version 3.

*** Release Notes ***

Things changed
--------------
* Project/Solution changes
The project name has been changed (mainly because the name 'DevExpress' 
is reserved for (guess who) 'DevExpress' controls. 
We found that anything that begins with 'DevExpress' in a project file
gets renamed by DevExpress's 'Project Converter' tool - by design.
Hence the project and solution were renamed to CABDevExpress.ExtensionKit
 - which I think is more appropriate anyhow.

 * Downloaded file changes
The release now includes precompiled CAB dll's (in the 'lib' folder)
which are referenced by the projects. This makes it easier to 
download and get the sample apps compiling and running.
You still need to generate your own 'license.licx' files by opening up
Controls with DevExpress controls on them (as always with DevExpress)

Things Added
------------
* Ribbon support:
Most of the Ribbon code is from the CAB SCSF DevExpress Extension Kit 
project, but it seems this project is not moving to CodePlex, so we've 
added the code here. Besides we think there should only be one 
CAB DevExpress Extension kit. The Code has been refactored and we've 
added a few more features to it. 

* XtraWindowWorkspace and XtraWindowSmartPartInfo:
To enable DevExpress XtraForm Skins but also to provide some dialog 
properties that vanilla CAB doesn't give you - so far we have added
'StartPosition' and 'ShowInTaskbar' - which you cannot set in CAB.
To get XtraForm skinning to happen, you need to do 3 things:
1) Your Program class needs to inherit from our XtraFormApplication eg:
internal class Program : XtraFormApplication<WorkItem, MainForm>
2) Your 'MainForm' (conveniently named 'MainForm' in the example above)
must inherit from DevExpress 'XtraForm'
3) Call DevExpress.Skins.SkinManager.EnableFormSkins() before 
Program().Run() in your main method.
The BankTeller App has all this, so check it for example usage.

* XtraTabSmartPartInfo - added the PageHeaderFont property.
XtraTabSmartPartInfo info = new XtraTabSmartPartInfo();
info.PageHeaderFont = new Font("Tahoma", 9.75F, FontStyle.Regular);
tabWorkspace.Show(smartPart, info);
Click on the 'Comments' button in the BankTeller ap to see this in action .

* DxMenuItemCommandAdapter - required if you want to create a CAB command 
handler for a DxMenuItem (ie a DevExpress pop-up menu). 
See CustomerAccountsView in the BankTeller app for sample usage.

* EditorButtonCollectionUIAdapter and EditorButtonCollectionUIAdapterFactory:
To enable adding the EditorButtonCollection as an UIExtension site. 
This way your modules can add buttons to the ComboBoxEdit and the LookupEdit. 

* NavigatorCustomButtonsUIAdapter and NavigatorCustomButtonsUIAdapterFactory. 
These allows you to add buttons to the XtraGridcontrol's Embedded Navigator. 

* RepositoryItemHyperLinkEditCommandAdapter - to hook up CAB commands to the 
OpenLink event on a DevExpress RepositoryItemHyperLinkEdit control.

See the Bankteller App for a demonstration of the CAB DevExpress Extension Kit.
There are no Ribbon examples this time, perhaps someone will help us out
and contribute an example. 

This release has only been tested with DXExperience 7.2.3, but should work 
with other versions too. This will not be a drop-in replacement of the 
first and  second drop as namespaces and project names have been changed. 

We will appreciate any comments or contributions.

------------------------------------------------------------------------------
Credits

Espen Schaathun (espen_schaathun@hotmail.com)
Vincent Guerci (puy0@hotmail.com) : NavBarWorkpace.
Peter van der Woude (spurrymoses@gmail.com) 
(XtraWindowWorkspace, XtraWindowSmartPartInfo, DxMenuItemCommandAdapter..)
------------------------------------------------------------------------------
