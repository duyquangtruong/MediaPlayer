   M S B A M L   `   `   `  ���� K  GMultimediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MultimediaPlayerMultimediaPlayer  !    MultimediaPlayer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:MultimediaPlayer     -     � Icon$  Images/icon1.png�� � Title$ Ultra Media��$	��600��$	��800��$	��500��$	��600�� � 	Resources    NoHoverButton �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� Triggers	 P� Property
 P� Value �� 
TargetName  GridViewColumnHeaderStyle1 ��Name �� Padding �� TextAlignment��%  &	        &	 �     �� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $��border��$��0q�$	 4,2q�
�������._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver
 . . ��� $ border�� � ��
 �   ��� " ��.���  � �� �� " ��.R��� $��0,0,0,1q�
�������
������ ._��� $ ContentHeader��#��z��$ 5,5,5,5q�#��z��$
 Left=�
���������  �@�OverridesDefaultStyle . ��  � ��
 �������  � ��$ Segoe UI)���  � ��$ 12(�.��� 
���   � � RowDefinitions �� $	��82* ��� $��5* ��� $	��13* � � ColumnDefinitions �� $��* �.+�� $��0�� �� -    ��Name$ PlayList��$��0* ��� -   $ Screen��$��6* �.+�� $��1�� �� -   $ 
videoFrame��$��* ��� -   $ 
musicFrame��$��0* �.+�r� -   $��mediaPlayer�� r� LoadedBehavior$ Manual=� r� UnloadedBehavior$ Manual=�$��0,10,0,0q�$��0���� -   $��musicBackground�� �� BitmapScalingMode$ HighQuality=�$��1���  �� $��1* ��� $��9* �.+�� $��0��$��Center=�$��	0,10,10,0q� �� $��9* ��� $��1* �.+���  �� Orientation$ 
Horizontal=�$��0��.+��� -   $��btnAdd��
���   �$��0q�$��30��$��30��$��15,0,0,0q� � ToolTip$	 Add��#��[  .���� $��Images/add.png��$ HighQuality=��� -	   $��	btnRemove��
���   �$��0q�$��30��$��30��$��5,0,00,0q�$ Remove��#��[  .���� $��Images/remove.png��$ HighQuality=��� -
   $��btnSave��$��30��$��30��
���   �$��0q�$��5,0,0,0q�$ Save Playlist��#��[  .���� $��Images/save.png��$ HighQuality=��� -   $��btnLoad��$��30��$��30��
���   �$��0q�$��5,0,0,0q�$ Load Playlist��#��[  .���� $��Images/load.png��$ HighQuality=��� -   $��btnClearAll��
���   �$��1��$��0q�$��30��$��30��$ 	Clear all��#��[  .���� $��Images/removeall.png��$ HighQuality=��� -   $��
lvPlayList��
���   �
�������
�������$��1��$��	0,10,0,28q� �� SelectionMode$ Single=����� " �� �� Triggers P� 	 �@J�IsMouseOver
 . . ���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 1q�P� 	 �@��
IsSelected
 . . ���  � ��
 ���� ��  � ��
 ���� ��  � ��$	 0.5q� �� View �� ! �� ColumnHeaderContainerStyle# [ P��� $
��Name�� �� DisplayMemberBinding �� *
SongName+� $��1�� �� $
��0.5* ��� $��7* ��� $
��0.5* �.+��� -   $ timeCurrent��$��0��$
��0:00��
�������$��Center=�$��Bottom=�$��0,0,0,2q��� -   $��
timeSlider�� J� Opacity$	 0.5Q�$��1��$��Center=� � Maximum$ 1Q�$��0,0,0,-6q� � LargeChange$
 0.05Q� �� TickFrequency$	 0.1Q�  � SmallChange$
  0.01Q��� -   $��timeEnd��$��2��$
��3:20��
�������$��Center=�$��Bottom=�$��0,0,0,2q�� $��2�� �� $	��99* ��� $
��198* ��� $
��198* ��� $
��100* ��� $
��198* �.+��� -   $��btnPlayList��
���   �
! � Tag$! 0��$��0q�$��30��$��30��$��0��#��[  $��34,22,35,22q�.���� -   $��imgPlaylist��$��Images/playlistoff.png��$ HighQuality=��� $ 
Horizontal=�$��Right=�$��1��.+��� -   $��	btnRepeat��
���   �$��0q�$! 0��$��20��$��20��$��0,0,10,0q�#��[  .���� -   $��	imgRepeat��$��Images/repeatoff.png��$ HighQuality=��� -   $��
btnShuffle��
���   �$��0q�$! 0��$��20��$��20��#��[  .���� -   $��
imgShuffle��$��Images/shuffleoff.png��$ HighQuality=��� -   $��btnStop��
���   �$��0q�$��30��$��30��$��10,0,0,0q�#��[  .���� -   $��imgStop��$��Images/stop.png��$ HighQuality=��� -   $��controlButton��$ 
Horizontal=�$��2��$��Center=�$��Center=�$
��27,7q�.+��� -   $��btnBack��
���   �$��0q�$��35��$��35��$��0,0,5,0q�#��[  .���� $��Images/backward.png��$ HighQuality=��� -   $��btnPlay��
���   �$��0q�$��50��$��50��#��[  $! 0��.���� -   $��imgPlay��$��Images/play.png��$ HighQuality=��� -   $��
btnForward��
���   �$��0q�$��35��$��35��#��[  .���� $��Images/forward.png��$ HighQuality=�� $��3��$
��Left=�$��2�� �� $��2* ��� $��8* �.+��� -   $��	btnVolume��$��0��
���   �$! 0��$��0q�$��20��$��20��$��0,0,10,0q�#��[  .���� -   $��	imgVolume��$��Images/speaker.png��$ HighQuality=��� -    $��sliderVolume��$��1��$��Center=�$��0,0,20,0q�$	 0.5Q�$	��100��$	��100��" � Value$" 10Q�