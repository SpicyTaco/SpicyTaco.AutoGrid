SpicyTaco.WpfToolkit
==================

A magical replacement for the built in WPF Grid and StackPanel.

> **NOTE:** I'm in the process of renaming this project from SpicyTaco.AutoGrid to SpicyTaco.WpfToolkit. This is because I plan to add more useful features to this package beyond just AutoGrid.

## Installation

To add SpicyTaco.WpfToolkit to your WPF project, all you have to do is install it from NuGet:

```
Install-Package SpicyTaco.AutoGrid
```

Usage Examples
--------------

**AutoGrid**

In order to get

![Form Image](v2_gridlayout.png)<br/>
[Sourced from the awesome WpfTutorials](http://wpftutorial.net/GridLayout.html)

You would typically write XAML that looked like:

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
        <RowDefinition Height="28" />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="200" />
    </Grid.ColumnDefinitions>
    <Label Grid.Row="0" Grid.Column="0" Content="Name:"/>
    <Label Grid.Row="1" Grid.Column="0" Content="E-Mail:"/>
    <Label Grid.Row="2" Grid.Column="0" Content="Comment:"/>
    <TextBox Grid.Column="1" Grid.Row="0" Margin="3" />
    <TextBox Grid.Column="1" Grid.Row="1" Margin="3" />
    <TextBox Grid.Column="1" Grid.Row="2" Margin="3" />
    <Button Grid.Column="1" Grid.Row="3" HorizontalAlignment="Right" 
            MinWidth="80" Margin="3" Content="Send"  />
</Grid>
```

You can simply write

```xml
<st:AutoGrid Rows="Auto,Auto,*,28" Columns="Auto,200" Orientation="Vertical">
    <Label Content="Name:"/>
    <Label Content="E-Mail:"/>
    <Label Content="Comment:"/>
    <Label /> <!-- Empty placeholder for lower left corner -->
    
    <TextBox Margin="3" />
    <TextBox Margin="3" />
    <TextBox Margin="3" />
    <Button HorizontalAlignment="Right" 
            MinWidth="80" Margin="3" Content="Send"  />
</st:AutoGrid>
```

I personally like to put my `Label`s with the element they are labeling. So just remove the `Orientation` which defaults to `Horizontal` and rearrange the elements. You can also pull the common margin up, defining it only once.

```xml
<st:AutoGrid Rows="Auto,Auto,*,28" Columns="Auto,200" ChildMargin="3">
    <Label Content="Name:"/>
    <TextBox/>

    <Label Content="E-Mail:"/>
    <TextBox/>

    <Label Content="Comment:"/>
    <TextBox/>

    <Label /><!-- Empty placeholder for lower left corner -->
    <Button HorizontalAlignment="Right" 
            MinWidth="80" Content="Send"  />
</st:AutoGrid>
```

**StackPanel**

The built in StackPanel control has always been frustrating to use. When you have a `TextBlock` that has a lot of text, it is impossible to wrap that text without setting an explicit width. Also, a StackPanel does not fill its container. 

Also, I've always wanted a simple container which would apply a margin but only between child elements. This allows me to control the margin of the parent and the spacing between each child separately and cleanly.

```xml
<st:StackPanel Orientation="Horizontal" MarginBetweenChildren="10" Margin="10">
    <Button Content="Info" HorizontalAlignment="Left" st:StackPanel.Fill="Fill"/>
    <Button Content="Cancel"/>
    <Button Content="Save"/>
</st:StackPanel>
```

Credits
-------

<img src="icon/icon_61620.png" alt="Icon" style="width: 128px; height: 128px;"/><br/>
[Furious designed by Matt Brooks from the Noun Project](http://thenounproject.com/Mattebrooks/icon/61620/)
