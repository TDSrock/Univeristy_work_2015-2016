name = room_get_name(room);
name = string(name) + "Save.ini";
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
    t=collision_circle(room_width/2,room_height/2,range,obj_Checkpoint,false,true);
    if t{
        ds_list_add(l,t);
        instance_deactivate_object(t);
    } else {
    break;
    }
}
ini_write_real('CheckPoint_count', 'Checkpoints', ds_list_size(l));
for(t=0;t<ds_list_size(l);t+=1){
    instance_operating = ds_list_find_value(l,t);
    instance_activate_object(instance_operating);
    ini_write_real('Checkpoint_x', string(t), instance_operating.x);
    ini_write_real('Checkpoint_y', string(t), instance_operating.y);
    ini_write_real('Mode', string(t), instance_operating.mode);
}
ds_list_destroy(l);
ini_write_real('Player', 'X', Player.x);
ini_write_real('Player', 'Y', Player.y);
ini_write_real('Player', 'Health', Player.lostHP);//add more player variables if deemed necisary
ini_close();
if (file_exists(name)){//Remove this check later.
    //show_message_async("Checkpoint Saved!");//Or edit this show mssage to display something else on the screen to clarify this event occuring
}
