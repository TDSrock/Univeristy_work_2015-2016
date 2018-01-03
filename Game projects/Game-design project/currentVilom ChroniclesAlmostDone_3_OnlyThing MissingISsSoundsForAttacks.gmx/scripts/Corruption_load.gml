name = room_get_name(room);
name = string(name) + ".ini";
if(!file_exists(name)){
    return show_message_async("File not found");
}
ini_open(name);
t = ini_read_real('Tile_count', 'Tiles',0);
for(i=0;i<t;i+=1){
    var inst = instance_place(ini_read_real('Tile_x', string(i), 0),ini_read_real('Tile_y', string(i),0),Par_Tile);
    if (inst >= 0){
        inst.corruption = ini_read_real('Tile_corruption', string(i), 20);
        if(inst.corruptionCompleteSteps != 0)
            inst.corruptionCompleteSteps = 0;
    }
}
ini_close();
//show_message_async("Loaded!");
