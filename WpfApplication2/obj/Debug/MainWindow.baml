   M S B A M L   `   `   `  ����J  FWpfApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication2WpfApplication2  5   	   G(clr-namespace:WpfApplication2.PropertiesWpfApplication2.Properties  5   	        WpfApplication2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:WpfApplication2     5   	   6p(clr-namespace:WpfApplication2.Properties     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   -   5          � Title$  TWM Airlines Management��5   	   $	��575��6)   $	��975��66    � Icon$ icon.ico��6Y   .��5
      �  J� RenderTransformOrigin$ 0.5,0.5&�6    � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   z�5   
   V� 5      .~�5      �� �� 5      �� 5       �� Angle$
 0.03Q�6"   T� 5      5      .+�5   
   o�  o� 
IsMainMenu . .@�5      m� $��_File��6   .@�5      m� -   5        $��_Connect��5      6-   m� 5      $��_New...��6   �� 5      m� 5      -   5        $��printButton��5      $��_Print��6.   {�.  6P   m� 5      $��_Save Table As...��6   .@�5       m� -   5        $��createPdfFile��5       $
��_PDF��64   {�.   m� Icon 5!      �� 5"      $j��dC:\Users\YER\documents\visual studio 2012\Projects\WpfApplication2\WpfApplication2\icons\PDFicon.jpg��6$   5$      m� 5%      -   5        $��createCsvFile��5%      $��_Excell��64   {�.   5&      �� 5'      $m��gC:\Users\YER\documents\visual studio 2012\Projects\WpfApplication2\WpfApplication2\icons\EXCELLicon.png��6$   5)      5*      �� 5+      m� 5,      -   5        $��_Exit��5,      6*   5-      m� 5.      $��_Tools��6   .@�5/      m� -   5        $	��add��5/      $	��Add��6&   {�6F   ��  �� ElementName$ createPdfFile�� �� Path$ 	IsEnabled�m� 50      -   5        $��update��50      $��Update��6)   {�6O   �� $ createPdfFile��$ 	IsEnabled�m� 51      -	   5        $��delete��51      $��Delete��6)   {�6Q   �� $ createPdfFile��$ 	IsEnabled��� 52      m� 53      -
   5        $��FlightsByClient��53      $��Flights by year��62   6K   m� 54      -   5        $��changeDollar��54      $��change Dollar��6/   6F   55      m� 56      -   5        $��_About��56      	 � ToolTip$ 	 About TWM Airlisnes System��6>   5;      �� 5<   
   -   5        $��language��5<      $	��120��6#   
 �� 
IsEditable
 .  �� 
IsReadOnly .  �� Text$ English��6S   $��Right=�6b   $	��Top=�6~   $��	0,10,10,0q�6�   .@�5=      �� -   5        $��English��5=      $��English��6*   6>   �� 5>      -   5        $��	Français��5>      $��	Français��6+   6@   �� 5?      -   5        $��Hebrow��5?      $��
עברית��6)   6;   5@      �� 5A   
   -   5        $��clock��5A      $��Right=�6   $��Bottom=�69   $��27��6T   $	��195��6`   
�������6l   �� 5B   
   $(��"Welcome to the TWM Airlines System��6   $	��Top=�6=   $��	50,16,0,0q�6U   $
��Left=�6h   $
��Bold"�6�   $��45��6�   $��24(�6�   $	��436��6�   �� 5D   
   -   5         ��Name$ clients��5D      $��Clients��6;   $��20(�6M   $
��Left=�6[   $��	50,60,0,0q�6v   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show clients table��6�   �� 5E   
   -   5        $ flights��5E      $��Flights��6;   $��20(�6M   $
��Left=�6[   $��
170,60,0,0q�6v   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show flights table��6�   �� 5F   
   -   5        $��	airplanes��5F      $��	Airplanes��6"   $��20(�66   $
��Left=�6D   $��
290,60,0,0q�6_   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show airplanes table��6�   �� 5G   
   -   5        $��workers��5G      $��Workers��6    $��20(�62   $
��Left=�6@   $��
410,60,0,0q�6[   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show workers table��6�   �� 5H   
   -   5        $��tickets��5H      $��Tickets��6    $��20(�62   $
��Left=�6@   $��
530,60,0,0q�6[   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show tickets table��6�   �� 5I   
   -   5        $��flight_lines��5I      $��Flight lines��6%   $��20(�6<   $
��Left=�6J   $��
650,60,0,0q�6e   {�.  $	��Top=�6�   $	��100��6�   $��50��6�   $	 show flight-lines table��6�   �� 5J   
   -   5        $��	staffings��5J      $��	Staffings��6"   $��20(�66   $
��Left=�6D   $��
785,60,0,0q�6_   {�.  $	��Top=�6�   $��85��6�   $��50��6�   $	 show staffings table��6�   &   System.Windows.Controls.DataGrid  5K   
   -   5        $��	data_grid��5K      $��
0,130,0,28q�6$   �� 5L   
   -   5        $��numOfRecords��5L      $��Bottom=�6$   $
��Left=�6?   $��27��6Z   $	��769��6f   
�������6r   "  FontAwesome.WPF.ImageAwesome  5M   
   -   5        $��circle��5M         Icon$ Spinner=�6(      Spin .  $y�Hidden=�6D      SpinDuration$ 4Q�6X   $��50��6i   $��50��6u   $��Center=�6�   $��Center=�6�   5N      5O      