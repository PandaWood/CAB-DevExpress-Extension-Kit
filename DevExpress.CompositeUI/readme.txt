------------------------------------------------------------------------------
CAB DevExpress Extension Kit - Third Drop
http://www.codeplex.com/CABDevExpress
------------------------------------------------------------------------------
Introduction

This is the third drop of the CAB DevExpress Extension Kit. 

This time we've added:

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

* DxMenuItemCommandAdapter - required if you want to create a CAB command 
handler for a DxMenuItem (ie a DevExpress pop-up menu)

* EditorButtonCollectionUIAdapter and EditorButtonCollectionUIAdapterFactory:
To enable adding the EditorButtonCollection as an UIExtension site. 
This way your modules can add buttons to the ComboBoxEdit and the LookupEdit. 

* NavigatorCustomButtonsUIAdapter and NavigatorCustomButtonsUIAdapterFactory. 
These allows you to add buttons to the XtraGridcontrol's Embedded Navigator. 

* RepositoryItemHyperLinkEditCommandAdapter - to hook up CAB commands to the 
OpenLink event on a DevExpress RepositoryItemHyperLinkEdit control.

See the Bankteller App for a demonstration of the CAB DevExpress Extension Kit.
There are no Ribbon examples this time, but I guess you guys can 
figure that one out. Besides if we released a Ribbon application we would 
probably have to register with Microsoft. 

This release has only been tested with DXExperience 7.2.0, but should work 
with other versions too. This should be a drop-in replacement of the first and 
second drop. If you used the CAB SCSF DevExpress Extension Kit project project, 
this will not be a drop in replacement as classes have been renamed and 
namespaces changed. 

We will appreciate any comments or contributions.

------------------------------------------------------------------------------
Credits

Espen Schaathun (espen_schaathun@hotmail.com)
Vincent Guerci (puy0@hotmail.com) : NavBarWorkpace.
Peter van der Woude (spurrymoses@gmail.com) 
(XtraWindowWorkspace, XtraWindowSmartPartInfo, DxMenuItemCommandAdapter..)
------------------------------------------------------------------------------
