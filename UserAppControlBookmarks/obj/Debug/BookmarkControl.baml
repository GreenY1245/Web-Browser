   M S B A M L   `   `   `  ����R  NUserAppControlBookmarks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UserAppControlBookmarksUserAppControlBookmarks  5      *    $UserAppControlBookmarks.UserControl1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:UserAppControlBookmarks     5        � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ BookmarkResources.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.7* �6   6,   �� 5      $
��0.5* �6   6,   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*    � ColumnDefinitions 5    
   �� 5!      $��* �6   6+   �� 5"      $��* �6   6+   �� 5#      $��0.03* �6   6/   �� 5$      $��* �6   6+   �� 5%      $��* �6   6+   .+�5(   
   �� -   5        $��AddBookmark��5(   6      ButtonStyle#��[  6   $��8��6I   $��0��6V   $��Add bookmark��6f   6�   �� 5)   
   -   5        $��	addFolder��5)   6   #��[  6   $��9��6G   $��0��6T   $��
Add folder��6d   6�   �� 5*   
   -   5        $��RemoveBookmark��5*   6   #��[  6   $��8��6L   $��4��6Y   $��Remove Bookmark��6i   6�   �� 5+   
   -   5        $��EditBookmark��5+   6   #��[  6   $��9��6J   $��5��6W   $��Edit Bookmark��6g   6�   �� 5-   
   -   5        $��newBookmarkURL��5-   ;     newItemsTextBox#��[ 6   $��8��6Q   $��1��6^   6p   �� 5.   
   -   5        $��	newFolder��5.   ;   #��[ 6   $��9��6L   $��1��6Y   6k   R� 50   
   -   5        $��treeview_bookmarks��50      $��2��6-   $��0��6A   $��8��6N   ��6_   �� ��51      �� 52       �� ItemsSource 6+   �� *Maps+.R�53      �� ��6    �� *Title+ � ContextMenu 54      �� 55      $��SolutionContext��6*   .@�56   "   m� $��DeleteContext��6G   -   5        $��Delete Folder��56   \   6u   57      59      5:      5=      �� 5?   
   $��8��6   $��2��6(   6:   �� 5A   
   -	   5        $��listview_bookmarks��5A      $��3��6-   $��8��6=   $��2��6N   ��5C      �� 5D      .R�5E      ]� .+�5F      �� $��sourceimage��6H     ListView_Image#��[ 6    ��6[   �� *
AddressURI+6{   �� 5G        
DomainBold#��[ 6$   ��6H   �� *Title+6a   �� 5H        Address#��[ 6$   ��6E   �� *	Address+6`   5I       �� Triggers 5K      �� 5L       �� Value$
 true��6C   	 �� Binding	 6&   �� *
SIDomain+.a�5M      �� 
 �� 
TargetName$
 sourceimage��6%    �� Property � �� �� Value$ Resources/sloflag.ico��6P   6p   5N      5Q      5T      5U      5V      