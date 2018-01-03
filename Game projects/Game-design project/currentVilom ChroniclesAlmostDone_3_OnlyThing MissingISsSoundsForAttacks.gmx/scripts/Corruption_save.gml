//Save corruption to an ini file.
name = room_get_name(room);
name = string(name) + ".ini";
if (file_exists(name)){
    file_delete(name)
}
ini_open(name);
var l,t;
l=ds_list_create();
var range = room_width;
if(room_width < room_height)
    range = room_height;
while 1{
    t=collision_circle(room_width/2,room_height/2,range,Par_Tile,false,true);
    if t{
        ds_list_add(l,t);
        instance_deactivate_object(t);
    } else {
    break;
    }
}
ini_write_real('Tile_count', 'Tiles', ds_list_size(l));
for(t=0;t<ds_list_size(l);t+=1){
    instance_operating = ds_list_find_value(l,t);
    instance_activate_object(instance_operating);
    ini_write_real('Tile_x', string(t), instance_operating.x);
    ini_write_real('Tile_y', string(t), instance_operating.y);
    ini_write_real('Tile_corruption', string(t), instance_operating.corruption);
}

ini_close();
if (file_exists(name)){
    //show_message_async("Saved!");
}
FILE  = file_text_open_write("Xvalues")
FILE2  = file_text_open_write("Yvalues")
for(t=0;t<ds_list_size(l);t+=1){
    instance_operating = ds_list_find_value(l,t);
    
    file_text_write_string(FILE, string(instance_operating.x) + "
");

    
    file_text_write_string(FILE2, string(instance_operating.y) + "
");

}
ds_list_destroy(l);
    file_text_close(FILE);
        file_text_close(FILE2);

