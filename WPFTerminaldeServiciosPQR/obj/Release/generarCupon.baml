   M S B A M L   `   `   `  ���� T  PWPFTerminaldeServiciosPQR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:WPFTerminaldeServiciosPQRWPFTerminaldeServiciosPQR  ,    &WPFTerminaldeServiciosPQR.generarCupon   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:WPFTerminaldeServiciosPQR       � Title$  CentroAguas��$	��900��$
��1600�� � WindowStyle$
 None=� � WindowState$ 	Maximized=�.��� ���#D8D8D8 � RowDefinitions �� $
��215* ��� $	��95* ��� $	��95* ��� $	��95* ��� $	��95* ��� $	��95* ��� $	��95* ��� $	��95* ��� $
��150* � � ColumnDefinitions �� �� �� .+��� -   $��spnl_encabezado��$��0��$��3��.+��� $	��216�����#00427A�� -   $��spnl_numeromatricula��$��0��$��1��$��	20,10,0,5q�.+��� $��30(�$	��500��$��50��$��Numero de matricula:���� -   $��spnl_txtnumeromatricula��$��2��$��
25,10,0,15q�.+��� -   $��txtNumeromatricula��$��30(�$	��300��$��50��$
��Left=��� -   $��spnl_nombresuscriptor��$��1��$��1��$��2��$��
100,10,0,5q�.+��� $��30(�$	��500��$��50��$
��Left=�$��Nombre del suscriptor:���� -   $��spnl_txtnombre��$��1��$��2��$��2��$��100,10,0,15q�.+��� -   $��	txtNombre��$��30(�{�.  $	��600��$��50��$
��Left=��� -   $��spnl_lblnumerofactura��$��3��$��	20,10,0,0q�.+��� $��30(�$	��500��$��50��$
��Left=�$$��Número de la última factura:���� -	   $��spnl_txtnumerofactura��$��4��$��	25,0,0,15q�.+��� -
   $��txtNumerofactura��$��30(�{�.  $	��300��$��50��$
��Left=��� -   $��spnl_lblsaldopendiente��$��1��$��3��$��
100,10,0,5q�.+��� $��30(�$	��500��$��50��$
��Left=�$��Saldo pendiente:���� -   $��spnl_saldopendiente��$��4��$��1��$��
100,0,0,15q�.+��� -   $��txtSaldopendiente��$��30(�{�.  $	��300��$��50��$
��Left=����� *Value+ �� StringFormat$ {0:#.00}���� -   $��spnl_lblfacturasconsaldo��$��2��$��3��$��	10,10,0,5q�.+��� $��30(�$	��500��$��50��$
��Left=�$��Facturas con saldo:���� -   $��spnl_facturasconsaldo��$��4��$��2��$��	10,0,0,15q�.+��� -   $��txtFacturasconsaldo��$��30(�{�.  $	��260��$��50��$
��Left=����� *Value+$ {0:#.00}���� -   $��spnl_lblvalorabonar��$��5��$��	20,10,0,5q�.+��� $��30(�$	��500��$��50��$
��Left=�$��Valor abonar:���� -   $��spnl_valorabonar��$��6��$��0��$��
25,10,0,15q�.+��� -   $��txtValorabonar��$��30(�$	��300��$��50��$
��Left=��� -   $��spnl_lblmensaje��$��0��$��7��$��2��$��10,0,0,0q�.+��� -   $��
lblmensaje��$��30(�$	��500��$��50��$
��Left=�$�� ���� -   $��spnl_btnImprimir��$��2��$��7��$��0,0,0,0q�.+��� $��30(�$��Imprimir��$	��150��$��50���� -   $��spnl_btnSalir��$��2��$��8��$��0,0,0,0q�.+��� -   $��30(�$��Regresar��$	��150��$��50���� -   $��piedepagina��$��8��$��3��$��0,50,0,0q�.+��� $
��1300��$��60��
����������#00427A$��0,20,0,0q�$��Center=� J� RenderTransformOrigin$ 
1.01,0.673&�$��Center=�$����Desarrollado por el Proceso de Gestión Informática – Centroaguas S.A. E.S.P. – Copyright 2017. – Todos los Derechos Reservados��