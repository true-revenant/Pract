   M S B A M L   `   `   `  ����K  GED_DesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ED_DesktopClientED_DesktopClient  5   	   M+clr-namespace:ED_DesktopClient.UserControlsED_DesktopClient.UserControls  5   	   !    ED_DesktopClient.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:ED_DesktopClient     5   	   DUserControls+clr-namespace:ED_DesktopClient.UserControls     5   	     � Title$  
MainWindow��5	   	   $��659.909��6   $��1159.451��6-    � WindowStartupLocation$ CenterScreen=�6>   .��5
      � $��0,0,2,4q�6    J� RenderTransformOrigin$ 0.446,0.374&�6    � ColumnDefinitions 5   
   �� 5      $
��198* �6   �� 5      $
��185* �6   .+�5   
   �� -   5         ��Name$ employeeListView��5      $��
10,10,2,10q�6-   ���	#FFEAEAEA �� SelectionMode$ Single=�5      ��5      �� *EmployeesList+ �� SelectedItem 69   �� *SelectedEmployee+ �� View 5      �� 5      .P�5      ��  �� Width$
 auto��6%   $��Полное Имя��62   	 �� DisplayMemberBinding	 6F   �� *
FullName+�� 5      $	 100��6%   $��Возраст��61   	 6B   �� *Age+�� 5      $	 100��6%   $��Стаж(в годах)��61   	 6H   �� *
StageYears+�� 5      $
 auto��6%   $��
Отдел��62   	 6A   �� *
DepTitle+5      5      7   1ED_DesktopClient.UserControls.EmployeeUserControl  5   
   -   5        
  Name$
 employeeUserControl��5   +   $��1��6H   $��10,10,0,114q�6X   �� 5   
   -   5         ��Name$ deleteButton��5      $��Удалить��6'   $��1��69   $��450,0,10,10q�6I   $��36��6^   $��Bottom=�6j   6�   �� 5   
   -   5        $ 
editButton��5      $��Изменить��6%   $��1��68   $
��Left=�6H   $��
348,0,0,10q�6c   $��97��6w   $��36��6�   $��Bottom=�6�   6�   �� 5   
   -   5        $ 	addButton��5      $��Добавить��6$   $��1��67   $
��Left=�6G   $��	10,0,0,10q�6b   $��99��6u   $��36��6�   $��Bottom=�6�   6�   5      5       