CAB DevExpress Extension Kit is a library that allows the use of DevExpress WinForm components
within CAB (Composite UI Application Block).

![](images/bankteller-skin.png)

### CAB Information
For more information on CAB see the the [SmartClient](http://www.codeplex.com/smartclient) project
 and read [Rich Newman's Intro to CAB](http://richnewman.wordpress.com/intro-to-cab-toc/) for a
 detailed and informative guide on each of CAB's components. Rich's articles would be essential
  reading for those thinking about contributing to the CABDevExpress.ExtensionKit.

_The BankTeller Sample Application (from the CAB distribution) modified to use DevExpress controls
 and the CABDevExpress.ExtensionKit:_


----------------------------------------------------

#### Recent Changes
- Removed IUIBarItemLinksAdapter (and implemented BarLinksOwnerCollectionUIAdapter : BarLinksCollectionUIAdapter
removed IRibbonGroupUI (wasn't used)

- Lots of small code clean-up in the Adapters (using Guard.ArgumentNotNull consistently; not checking for null if
it's unnecessary, or doing if it is; wrong class name used in XML comments etc)

- Removed the InternalCollection property for most UIElementAdapters (most of them weren't used, nor should be)

- Fixed XtraNavBarWorkspace.Close() bug where NavBarGroups were not removed

#### Things Added

- Ribbon support Added
- RibbonApplicationMenuUIAdapter
- RibbonPageHeaderUIAdapter
- RibbonQuickAccessToolbarUIAdapter
- RibbonGalleryUIAdapter and RibbonGalleryGroupUIAdapter (thanks to DevLynx)

Tests have been written and included using XUnit (not very comprehensive as yet, but it's a start)
see the XUnit CodePlex site at https://github.com/xunit/xunit

Note: there is a "ReSharper" plugin for XUnit at https://github.com/xunit/resharper-xunit


We appreciate any comments or contributions.

------------------------------------------------------------------------------
###### Contributors:

- Espen Schaathun espen_schaathun@hotmail.com
- Vincent Guerci puy0@hotmail.com
- Peter van der Woude (PandaWood) pandawoude@gmail.com
- DevLynx http://DevLynx.LiveJournal.com
