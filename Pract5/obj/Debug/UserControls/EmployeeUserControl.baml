   M S B A M L   `   `   `  ����K  GED_DesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:ED_DesktopClient.UserControlsED_DesktopClient.UserControls  5      7    1ED_DesktopClient.UserControls.EmployeeUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:ED_DesktopClient.UserControls     5      .��5	      � .+�5
   
   �� $
��Left=�6   $��	65,10,0,0q�6/   $
��Wrap=�6B   $��ИМЯ��6V   $	��Top=�6a   $��22��6y     �� TextAlignment$  Center=�6�   �� 5   
   $
��Left=�6   $��	31,42,0,0q�6/   $
��Wrap=�6B   $��ФАМИЛИЯ��6V   $	��Top=�6e   $��22��6}   �� 5   
   -   5         ��Name$ nameTextBox��5      $��23��6'   $��	97,9,10,0q�63    �� TextWrapping$
 Wrap=�6F   $	��Top=�6Z   ��6r   �� *Emp.FirstName+�� 5   
   -   5        $ lastnameTextBox��5      $��22��6+   $��
97,42,10,0q�67   $
 Wrap=�6K   $	��Top=�6_   ��6w   �� *Emp.LastName+�� 5   
   -   5         ��Name$ depsComboBox��5      $��
97,77,10,0q�6)   $	��Top=�6=    �� SelectedItem 6U   �� *	Emp.Dep+��6v   �� *Deps+�� 5   
   $
��Left=�6   $��	54,77,0,0q�6/   $
��Wrap=�6B   $��
ОТДЕЛ��6V   $	��Top=�6c    J� RenderTransformOrigin$ 0.478,0.257&�6{   $��22��6�   �� 5   
   -   5        $ stageTextBox��5      $��23��6(   $��97,114,10,0q�64   $
 Wrap=�6I   $	��Top=�6]   ��6u   �� *Emp.StageYears+�� 5   
   $
��Left=�6   $��
10,115,0,0q�6/   $
��Wrap=�6C   $��СТАЖ (в годах)��6W   $	��Top=�6m   $��23��6�   �� 5   
   -   5         ��Name$ activeCheckBox��5      $��АКТИВЕН��6+   $
��Left=�6=   $��
97,153,0,0q�6X   $	��Top=�6l   $��28��6�    d� 	IsChecked 6�   �� *Emp.IsActive+5      5      