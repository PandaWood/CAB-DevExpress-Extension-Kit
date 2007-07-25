------------------------------------------------------------------------------
CAB DevExpress Extension Kit - Third Drop
http://www.codeplex.com/CABDevExpress
------------------------------------------------------------------------------
Introduction

Here is the third drop of the kit. 

This time we've added Ribbon support, most of the Ribbon code is from the 
CAB SCSF DevExpress Extension Kit project but it seems like that 
project is not moving to CodePlex so we've added the code here. Besides we 
think we only should have one CABDEvExpress kit. The Code has been refactored 
and we've added a few more features to it. 

The new XtraWindowWorkspace is added to get DevExpress XtraForm Skins to 
show. This is enabled by callingDevExpress.Skins.SkinManager.EnableFormSkins(); 
before Program().Run; in your main method.

We've also added a MenuItemCommandAdapter which is required if you want to 
create a pop-up menu in a XtraGrid. By using the MenuItemCommandAdapter together 
with a DXMenuItem you'll be able to hook up CAB Command handlers to it. 

Added EditorButtonCollectionUIAdapter and EditorButtonCollectionUIAdapterFactory
so that it's possible to add the EditorButtonCollection as an UIExtension site. 
This way your modules can add buttons to the ComboBoxEdit and the LookupEdit. 

Also added  NavigatorCustomButtonsUIAdapter and 
NavigatorCustomButtonsUIAdapterFactory. This allows you to add buttons to the 
XtraGridcontrol's Embedded Navigator. 

See the Bankteller App for a demonstration of the ExtensionKit in action. No 
Ribbon examples this time, but I guess you guys can figure that one out. Besides
if we released a Ribbon application we would probably have to register with 
Microsoft . 

This release has only been tested with DXExprience 7.2.0, but should work 
with other versions too. This should be a drop-in replacement of the first and 
second drop. if you used the CAB SCSF DevExpress Extension Kit project project, 
this will not be a drop in replacement as classes has been renamed and 
namespaces has been changed. 

We will appreciate any comments or contributions.

------------------------------------------------------------------------------
Credits

Espen Schaathun (espen_schaathun@hotmail.com)
Vincent Guerci (puy0@hotmail.com) : NavBarWorkpace.

Special thanks to:
Spurry Moses for contributing the XtraWindowWorkspace, XtraWindowSmartPartInfo
	     and the MenuItemCommandAdapter

The CAB SCSF DevExpress Extension Kit project for the Ribbon code. 

------------------------------------------------------------------------------
