with(Player)
{
    scr_theme_swap(sound_Tanis_Play_Theme);
   mode = 1;
   lostHP = 0;
   maxStamina = 600;
   stamina = maxStamina;
   y-=20
}
scr_Checkpoint_save();
show_message_async("You have completed the first half of the game! Now you shall play as Tanis, the knight that wants to fix the wrongs that have been done by Vairl.");
var l,t;
l=ds_list_create();
while 1{
    t=collision_circle(room_width/2,room_height/2,room_width/1.8,Par_Tile,false,true);
    if t{
        ds_list_add(l,t);
        instance_deactivate_object(t);
    } else {
    break;
    }
}
for(t=0;t<ds_list_size(l);t+=1){
    instance_operating = ds_list_find_value(l,t);
    instance_activate_object(instance_operating);
    instance_operating.corruption +=40;//Level complete corruption for now.
    instance_operating.coruptionOff = true;
}
ds_list_destroy(l);
var i,k;
i=ds_list_create();
while 1{
    k=collision_circle(room_width/2,room_height/2,room_width/1.8,Par_Enemy,false,true);
    if k{
        ds_list_add(i,k);
        instance_deactivate_object(k);
    } else {
    break;
    }
}
for(k=0;k<ds_list_size(i);k+=1){
    instance_operating = ds_list_find_value(i,k);
    with(instance_operating){
        instance_destroy();
    }
}
ds_list_destroy(i);
var i,k;
i=ds_list_create();
while 1{
    k=collision_circle(room_width/2,room_height/2,room_width/1.8,enemy_spawner,false,true);
    if k{
        ds_list_add(i,k);
        instance_deactivate_object(k);
    } else {
    break;
    }
}
for(k=0;k<ds_list_size(i);k+=1){
    instance_operating = ds_list_find_value(i,k);
    instance_activate_object(instance_operating);
    instance_operating.enemies_allowed = 1;
}
ds_list_destroy(i);
