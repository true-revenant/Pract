   M S B A M L   `   `   `  ����K  GED_DesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ED_DesktopClient.WindowsED_DesktopClient.Windows  5   	   *    $ED_DesktopClient.Windows.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ED_DesktopClient.Windows     5   	     � Title$  LoginWindow��5   	   $	��229��6   $	��229��6*   $	��229��66   $	��229��6E    � 
ResizeMode$ NoResize=�6U    � WindowStartupLocation$ CenterScreen=�6k   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��161* �6   �� 5      $	��62* �6   .+�5   
   �� -   5         ��Name$ loginTextBox��5      $��23��6(   $��
10,49,12,0q�64    �� TextWrapping$
 Wrap=�6H   $	��Top=�6\   $��2��6t   $��0��6�   �� 5   
   $
��Left=�6   $��	10,28,0,0q�6/   $
��Wrap=�6B   $��Логин:��6V   $	��Top=�6d    J� RenderTransformOrigin$ 0.446,0.313&�6|   �� 5   
   $
��Left=�6   $��	10,94,0,0q�6/   $
��Wrap=�6B   $��Пароль:��6V   $	��Top=�6e   �� 5   
   -   5         ��Name$ cancelButton��5      $��Отмена��6'   $��118,154,12,12q�68   $��2��6j   $��3��6~   �� 5   
   -   5        $ loginButton��5      $��Вход��6&   $
��Left=�65   $��10,154,0,12q�6P   $��94��6e   $��2��6�   G� 5   
   -   5         G�Name$ passwordBox��5      $��10,115,12,0q�6+   $	��Top=�6@   $��23��6X   $��2��6d   $��1��6x   �� 5   
   $��Right=�6   $��0,4,12,0q�60   $
��Wrap=�6B   $��ED��6V   $	��Top=�6`   $��34��6x   $��29��6�   $��22(�6�   $��1��6�   5      5      