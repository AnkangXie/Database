   M S B A M L   `   `   `  ����C  ?MusicApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MusicApp.ControlMusicApp.Control  5      &     MusicApp.Control.SongInfoControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:MusicApp.Control     5        � 	Resources  5         MusicPictureStyle �� 
TargetType �� Property �� Value � Cursor �� CornerRadius �� ImageSource J� Opacity J� Effect-  'System.Windows.Media.Effects.BlurEffect	   Radius  BottomButTextColor  Iconfont
 �� Triggers P� Property P� Value �� 
TargetName  MusicNameButtonStyle �� 
TargetType  TopStoryboard g� Duration	 W� To �� 
TargetName �� TargetProperty  BottomStoryboard��%�  &	        &	 .    &	     &	 R    �� 5   
   " ��6*   .�5      ��  � �� 5      �� 5      .R�5      � $
 Hand��6   .+�5      �� $ 5��6%   ��5   "   �� 5   &    61   �� *Model.SongModel.LocalPicUrl+6g   5      �� 5      $��border��6%   $
}�True��63   $ 5��6G   $	 0.9Q�6X    5   "     5   &   $	 3Q�61   ��5   "   �� 5    &    61   �� *Model.SongModel.LocalPicUrl+6g   5"      �� 5#      $
��text��6(   $��20(�5$   (   #���  5%   (   #���  5&   (   $��Center=�5'   (   $��Center=�5(   (   $	����5)   (   5+      
 5,      P� 5-       �@J�IsMouseOver$ False��6=   . �5.   "   �� $ border��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5/   "   $
 text��6Q    � y�$ 	Collapsed=�6?   6Q   50      52      54      55      �� 57   
   " ��6-   .�58      ��  � �� 59      �� 5:      " ��6&   .R�5;      �� #��z��6$   $��15(�6E   #���  6S   
 5<      P� 5=       �@J�IsMouseOver$
 true��6=   5>      5@      5B      5C      �� 5E   
   .��5F      W� $ 	0:0:0:0.5D�6    063   $ topTranslateTransform��6:   $ Y�6h   W� 5G      $ 	0:0:0:0.5D�6    063   $ bottomTranslateTransform��6:   $ Y�6k   W� 5H      $ 	0:0:0:0.2D�6    063   $ lyricsTranslateTransform��6:   $ Y�6k   5I      �� 5K   
   .��5L      W� $ 	0:0:0:0.5D�6   $ topTranslateTransform��63   $ Y�6a   W� 5M      $ 	0:0:0:0.5D�6   $ bottomTranslateTransform��63   $ Y�6d   W� 5N      $ 	0:0:0:0.1D�6   $ lyricsTranslateTransform��63   $ Y�6d   5O      5P       � Triggers 5Q      :� 5S   
    :� RoutedEvent$ Button.Click��6    :� 
SourceName$ SongPic��62   .[�5T      �� #���  6   5U      :� 5W   
   $ Button.Click��6   $ 	BottomBut��62   .[�5X      �� #���  6   5Y      .��5[      �  � RowDefinitions 5\   
   �� 5]      �� 5^      $��75 �6   .+�5b   
   � $
}�True��6   .+�5c      �� -   5         ��Name$ LyricsContent��5c      $
}�True��6,   z�5d      V� 5e      .~�5f      T� -   5        lyricsTranslateTransform�� T� Y 5f   O   ��  �� ElementName$ LyricsContent�� �� Path$ ActualHeight�5g      ._�5i      � .+�5j      ��  5k        5l      $	 20Q�6)   ��5n      �� 5o       6)   �� *Model.SongModel.LocalPicUrl+5q      �� 5r        MainPopupBackColor#���  6   $	 0.9Q�6O   6_   (   "MusicApp.Control.SongDetailControl  5t      -   5          Name$ 
SongDetail��5t   .   5u      5v      5w      �� 5z   
   $��1��6   .+�5|      ��  �� Orientation$ 
Horizontal=�6   $��75��62   z�5}      T� 5~      -   5        topTranslateTransform��$	 -75Q�5~   H   .+�5�      �� -   5        $��	BottomBut��5�      $
 Hand��6*   ��5�      �� 5�      .R�5�      �� $��40��6%   $��40��61     BottomBackColor#���  6<   $��	10 0 20 0q�6k   ._�5�   "   �� $	����6,   #���  5�   $   #���  5�   $   $��Center=�5�   $   $��Center=�5�   $   $��18(�5�   $   5�      5�      5�      5�      �� 5�      $ 
Horizontal=�6   $��75��62   y�6>   �� *Model.SongInfoVisibility+z�5�      T� 5�      -   5        bottomTranslateTransform��$	 -75Q�5�   K   6U   .+�5�      � .+�5�      �� #���  6   $ 5��6L   $��50��6]   $��50��6h   $��10 0 0 0q�6t   ._�5�      �� $	����6$   #���  5�      $��35(�5�      $��Center=�5�      $��Center=�5�      #���  5�      5�      �� 5�      -   5         ��Name$ SongPic��5�      $��50��6.   $��50��69   #���  5�      $��2q�5�      $��10 0 0 0q�5�      #���   5�      y�5�      �� *Model.SongPicVisibility+5�      5�      �� 5�      $��10 3 0 0q�6   $��Center=�6/   .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� #���  6!   $
 Hand��5�      $	��100��5�      ��5�   '   �� *Model.SongModel.SongName+5�      �� 5�      -   5        $
 Hand��5�   $   $��5 2 0 0q�5�      #���  5�      #���  5�      $��18(�5�      $	����5�      $��Center=�5�      5�      5�      �� 5�      $
 Hand��6   ��5�      �� 5�      .R�5�   "   �� $��13(�6,   $��0 5 0 0q�5�   %   #���  5�   %   $��Light"�5�   %   ��5�   %   �� *Model.SongModel.Author+5�      5�      5�      5�      5�      5�      5�      