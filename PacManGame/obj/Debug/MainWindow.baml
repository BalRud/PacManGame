   M S B A M L   `   `   `  ����E  APacManGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PacManGame
PacManGame  5   	       PacManGame.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PacManGame     5   	   -   5          � Title$  PacMan Game��5   	    � SizeToContent$ WidthAndHeight=�6    � 
ResizeMode$ NoResize=�6U   
���   �.��5	      � -   5         �Name$ grdMain��5	      $
��1000��6   $	��805��6)    � ShowGridLines .  
�������.+�5
   
   � -   5        $ grdLevel��5
      $
��1000��6!   $	��750��6.    .  
�������$	��Top=�6e   �� 5   
   -   5         ��Name$ cnvLevel��5      $	��750��6#   $
��1000��60   $	��Top=�6=   �� 5   
   -   5         ��Name$ 
lb_Cleared��5      $��Level Cleared��6$   $��72(�6<   $
��Bold"�6J   $��Italic$�6\   $��Center=�6o   $��Center=�6�   $y�Hidden=�6�   �� 5   
   -   5        $ lb_Game_Over��5      $��	Game Over��6&   $��72(�6:   $
��Bold"�6H   $��Italic$�6Z   $��Center=�6m   $��Center=�6�   $y�Hidden=�6�   � 5   
   -   5        $ grdScore��5      $
��1000��6!   $��55��6.    .  
�������$��Bottom=�6j   .+�5      �� -   5        $ lb_Score��5      $��Score: 0��6&   $
��Left=�69   $	��Top=�6T   $��24(�6l   $
��Bold"�6z   $��52��6�   $	��170��6�   �� 5      -	   5        $ lb_Life��5      $��Lives: 0��6%   $��Right=�68   $	��Top=�6T   $��24(�6l   $
��Bold"�6z   $��52��6�   $	��125��6�   5      5      5      