------------------------------------------------------------------------------
CABDevExpress.ExtensionKit 3.1
http://www.codeplex.com/CABDevExpress
------------------------------------------------------------------------------

*** Release Notes ***

Things changed
--------------
* Removed IUIBarItemLinksAdapter (and implemented BarLinksOwnerCollectionUIAdapter : BarLinksCollectionUIAdapter 
removed IRibbonGroupUI (wasn't used)

* Lots of small code clean-up in the Adapters (using Guard.ArgumentNotNull consistently; not checking for null if 
it's unnecessary, or doing if it is; wrong class name used in XML comments etc)

* Removed the InternalCollection property for most UIElementAdapters (most of them weren't used, nor should be)

* Fixed XtraNavBarWorkspace.Close() bug where NavBarGroups were not removed


Things Added
------------
* Ribbon support Added 
1) RibbonApplicationMenuUIAdapter
2) RibbonPageHeaderUIAdapter
3) RibbonQuickAccessToolbarUIAdapter

* Tests have been written and included using XUnit (so we can refactor code with some confidence)
see the XUnit CodePlex site at http://www.codeplex.com/xunit 
Note there is a ReSharper plugin for XUnit at http://www.codeplex.com/xunitext/Release/ProjectReleases.aspx


We will appreciate any comments or contributions.

------------------------------------------------------------------------------
Contributors:

Espen Schaathun (espen_schaathun@hotmail.com)
Vincent Guerci (puy0@hotmail.com)
Peter van der Woude (spurrymoses@gmail.com)
