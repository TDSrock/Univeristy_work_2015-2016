if(attack_cooldown_curr == 0)
{
    list = ds_list_create();
    distance_to_player = point_distance(x,y,Player.x, Player.y);
    if(distance_to_player > max_range)
        AI_mode = 1;
    else if (distance_to_player < pref_min_range && CurHP/MaxHP > 0.25 && random(1)<0.85)
        AI_mode = 3;
    
    for(i = 0; i < ds_list_size(attacks); i++)
    {
        curr_check = ds_list_create();
        curr_check = ds_list_find_value(attacks, i);
        curr_check_max = ds_list_find_value(curr_check, 0);
        curr_check_min = ds_list_find_value(curr_check, 5);
        if(curr_check_max>distance_to_player && curr_check_min<distance_to_player)
        {
            ds_list_add(list, curr_check);
        }
    }
    if(ds_list_size(list)!=0)
    {
        current_attack = ds_list_create();
        current_attack = ds_list_find_value(list, floor(random(ds_list_size(list))));
        time_to_attack = ds_list_find_value(current_attack, 7);
        self_attack = false;
        attack_cooldown_curr = attack_cooldown;
        AI_mode = 2;
        image_index = 0;
    }
    ds_list_destroy(list);
}
else
{
    attack_cooldown_curr--;
}
