   M S B A M L   `   `   `  ����  MusicApp-    'MusicApp.Control.Widget.CarouselControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a;	behaviors+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5         NavButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius � Cursor  Iconfont  RadioButStyle ��Name  FoundMusicRButColor �� Triggers	 P� Property
 P� Value �� 
TargetName  MainLineBackColor  CenterImageStoryboard g� FillBehavior g� Duration	 W� To �� 
TargetName �� TargetProperty g� 	BeginTime �� KeyTime �� Value��%y  &	        &	 L    &	 �    �� 5   
   " ��6'   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 50��6!   ���	#1e1e1e00$
 Hand��5   !   ._�5      �� #��z��6(   ���#ffffff$��13(�5   (   $
��Bold"�5   (   #���  5   (   $��Center=�5   (   $��Center=�5   (   5      5      5      5       �� 5"   
   " 	�6&   .�5#      ��  � �� 5$      �� 5%      " 	�6&   .R�5&      �� $ border��6!   $
 Hand��61   $ 50��6?   #���  5'   !   $��5��5(   !   $��5��6+   $��8q�66    5)      P� 5*      	 �@d�	IsChecked$

 trueY�6;   . �5+   "   �� $ border��6?   # �  6S    � ��5,      5.      50      51      �� 52   
   .��53      W� $
 Stop=�6   $ 	0:0:0:0.5D�62    1006G   $ LeftImageTransform��6P   $ X�6{   6�   W� 54      $
 Stop=�6   $ 	0:0:0:0.5D�62    2006G   $ 	LeftImage��6P   $ Height�6r   6�   �� 55      $
 Stop=�6+    00:00:006?   $ 	LeftImage��6T   $ (Panel.ZIndex)�6v   .C�56      �� $ 00:00:00.00��6&   $ 0��6<   �� 57      $ 00:00:00.02��6&   $ 2��6<   58      W� 5:      $
 Stop=�6   $ 	0:0:0:0.5D�62    1006G   $ CenterImageTransform��6P   $ X�6}   6�   W� 5;      $
 Stop=�6   $ 	0:0:0:0.5D�62    1506G   $ CenterImage��6P   $ Height�6t   6�   5<      5=      .��5>      �� .+�5?   
   � -   5         �Name$ 
Contrainer��5?      $
��1200��6#   $��20q�63   $	��200��6?   .+�5@      �� -   5        $ 	LeftImage��5@      $	��150��6(   $	��600��65   $ 10��6A     MainBackColor#���  6S    K� ZIndex$ 0��6�   $��Center=�5A      $
��Left=�60   z�5B      T� 5C      -   5        LeftImageTransform�� T� X$ 0Q�5C   E   ._�5E      � .+�5F      �� $
��Fill=�6?   ��6   �� *Model.LeftImage+ J� Clip 5G      �� 5H       �� RadiusX$ 10Q�60    �� RadiusY$ 10Q�6=    �� Rect$ 0,0,600,150�6J   5J      �� 5K      -   5         ��Name$
 Back��5K      $��10 0 0 0q�5L      $��30��5M      $��30��5N      #���   5O      $	����5P      $
��Left=�5Q      ��5R      �� *BackClickCommand+5S      5T      �� 5U      -   5        $ CenterImage��5U      $	��200��6*   $	��600��67   $	��500��6F   $ 10��6U   $
 Hand��6g   $��Center=�5V      $��Center=�62   #���  5W      $ 1��6B   z�5X      T� 5Y      -   5        CenterImageTransform��$ 0Q�5Y   G   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5[      +  %Microsoft.Xaml.Behaviors.EventTrigger  5\         	EventName$ 	MouseDown��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5]      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *CenterMouseDownCommand+5^      ._�5`      � $
}�True��6   .+�5a      �� $	����6      MainOverBackColor#���  5b      $	��100(�5c      $��Center=�5d      $��Center=�5e      #���  5f      �� 5g      $
��Fill=�6A   ��6   �� *Model.CenterImage+ 5h      �� 5i      $ 10Q�60   $ 10Q�6=   $ 0,0,600,200�6J   5k      �� 5l      $ 5 0 5 0��6   $	��200��6`   $��25��6o     �� Padding$  5q�6{   $��Right=�6�   $��Bottom=�6�   ��64   �� *Model.CenterTextBack+._�5m      �� $��12(�5n       ���#ffffff$��Center=�5p       $��Center=�5q       ��5m   $   �� *Model.CenterText+5q       5r      5s      5t      �� 5u      $	��150��6   $	��600��6"   $ 10��6.   #���  6@   $
}�True��6m   $��Right=�6�   $��Center=�6�   ._�5v      � .+�5w      �� $
��Fill=�6@   ��6   �� *Model.RightImage+ 5x      �� 5y      $ 10Q�60   $ 10Q�6=   $ 0,0,600,150�6J   5{      �� 5|      -   5        $ Forward��5|      $��0 0 10 0q�5}      #���   5~      $	����5      $��30��5�      $��30��5�      #���  5�      $��Right=�5�      ��5�      �� *ForwardClickCommand+5�      5�      5�      �� 5�   
   -   5        ! ��Name$! ButContrainer��5�      " �� Orientation$" 
Horizontal=�6,   $��Center=�6E   5�      5�      