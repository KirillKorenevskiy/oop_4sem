   M S B A M L   `   `   `  ����A  =lab4_5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab4_5lab4_5  5          lab4_5.LanguageButton   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:lab4_5     5      .��5	      � .+�5
   
   �� -   5        $	��200��5
      $��50��6   
������ 
�������
������ $��0q�5      $��Center=�6%   $��Center=�6B   ��5      � 5        � ColumnDefinitions  5      �� 5      $
��Auto �6+   �� 5      $
��Auto �6+   .+�5      �� $��5q�6x   $��1��6�   ��6    �� *LanguageText+ �� RelativeSource ��  �� AncestorType$ UserControlL�6�   5      ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      :� 5       :� RoutedEvent$ Button.Click��6'   .[�5      �� .��5   "   �� .��5   &   W�  �� TargetProperty$9 3(UIElement.RenderTransform).(RotateTransform.Angle)�66    W� From$ 0X�5   6   	 W� To$	 360X�6?   	 g� Duration$	 0:0:1D�5   6   
 g� RepeatBehavior$
 1x��5   6   5   #   5       5!      5#      z�5%      �� 5&      5(      5)      5*      