   M S B A M L   `   `   `  ����K  GSpletniBrskalnik, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SpletniBrskalnikSpletniBrskalnik  5          SpletniBrskalnik.Tab   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:SpletniBrskalnik     5      -   5        .��5	      �   � ColumnDefinitions  5   
   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $	��20* �6   6+   �� 5      $��25 �6   6*   .+�5   
   ]� -   5        $��
db_tabitem��5   !   $��0��6   ��. .+�5      �� -   5        $��addBookmarkButton��5      
�������$��Right=�6A   ��6X   Z� ��6o   Z� ��5      �� 5      $��images/bookmark.png��6   6;   5      �� 5      -   5        $��backB��5   (   
�������$��2��65   $
��Left=�6I   $
��Bold"�6_   $��0q�6�    � ToolTip$ Go Back��6�   ��6�   Z� ��6�   Z� ��5      �� 5      $��images/backward.png��6   6;   5       �� 5!      -   5        $��forwB��5!   (   
�������$��2��65   $
��Left=�6I   $
��Bold"�6_   $��0q�6�   $ 
Go Forward��6�   ��6�   Z� ��6�   Z� ��5"      �� 5#      $��images/forward.png��6   6:   5%      �� 5&      -   5        $��refrB��5&   (   
�������$��2��65   $
��Left=�6I   $
��Bold"�6_   $��0q�6�   $ Reload��6�   ��6�   Z� ��6�   Z� ��5'      �� 5(      $��images/reload.png��6   69   5*      �� 5+      -   5        $��backHome��5+   (   
�������$��0q�68   ��6L   Z� ��6c   Z� ��5,      �� 5-      $��images/home.png��6   67   5/      �� 51      -   5        $��	searchBox��51   O      	SearchBox#��[  6   $
��left=�69   .��6�   52      53      �� 55   
   -	   5        $"��bookmark_mainwindow_listview��55   �   $��Disabled=�6   $��1��6I   $	��Top=�6o   ��56      �� 57      .R�58      ��  �� Orientation$ 
Horizontal=�6!   .+�59      �� $��sourceimage��6    $
��20px��6Q   ��63   �� *
AddressURI+6d   �� 5:      $��Center=�6$   $��5,0,0,0q�6V   ��6?   �� *Title+6i   5;       �� Triggers 5=      �� 5>       �� Value$
 true��6C    �� Binding 6&   �� *
SIDomain+.a�5?      ��  �� 
TargetName$ sourceimage��6%    �� Property � ��	 �� Value$	 Resources/sloflag.ico��6P   6p   5@      5C      ��5F      �� 5G      .R�5H      �� $ 
Horizontal=�6!   6<   5I      5K      (  "System.Windows.Controls.WebBrowser  5M   
   -
   5        $��browserSource1��5M   "   $��2��6   
 � Cursor$
 Arrow��68   $��2��6f   6�   �� 5O   
   -   5        $	��sb_��5O   !   $��3��6   $��25��6,   $��25��6;   .@�6J   .@�5Q      Browser 1.0 : �� �� 5R      -   5        $��sb_it1��5R      .��5S      �� -   5        $��
txB_sb_it1��5S      60   5T      5U      5V      5W      