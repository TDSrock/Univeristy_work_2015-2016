name = room_get_name(room);
name = string(name) + "Save.ini";
if(!file_exists(name)){
    return show_message_async("File not found");
}
ini_open(name);
t = ini_read_real('CheckPoint_count', 'Checkpoints', 0);
for(i=0;i<t;i+=1){
    var inst = instance_place(ini_read_real('Checkpoint_x', string(i), 0),ini_read_real('Checkpoint_y', string(i),0),Par_Tile);
    if (inst >= 0)
        inst.mode = ini_read_real('Mode', string(i), 0);
}
Player.x = ini_read_real('Player', 'X', 0);
Player.y = ini_read_real('Player', 'Y', 0);
Player.lostHP = ini_read_real('Player', 'Health', 0) 
ini_close();
Corruption_load();
inst = instance_place(Player.x,Player.y,obj_Checkpoint)
with (inst){
scr_corruption(400, 40, 0, 100, x, y, 2);
}
//show_message_async("Loaded!");
