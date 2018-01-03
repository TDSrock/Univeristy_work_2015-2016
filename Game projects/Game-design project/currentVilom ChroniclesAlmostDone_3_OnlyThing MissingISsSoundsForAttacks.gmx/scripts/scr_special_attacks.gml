/*if(current_attack == AttackController.attack_dash)
{
    if(playerDir<270&&playerDir>90)
        hsp -= 200;
    else
        hsp += 200;
}*/

if(current_attack == AttackController.attack_minion_move_attack&&image_index = 0&& attacked)
{
    if(image_xscale = -1)
    {
        x+=150
    }
    else
    {
        x-=150;
    }
}

if(current_attack == AttackController.attack_zato_tel_kick)
{
    if(image_index == 1)
    {
        kickx = Player.x;
        kicky = Player.y;
    }
    if(image_index == time_to_attack)
    {
        foot = instance_create(kickx, kicky, obj_zato_tel_kick_foot);
        foot.damage = ds_list_find_value(current_attack, 1);
        foot.image_xscale = image_xscale;
        foot.image_speed = image_speed;
        with(foot)
        {
            while(!place_meeting(x,y,Par_Tile))
            {
                y++;
            }
            y--;
        }
        attack_cooldown_curr += 2*room_speed;
        attacked = true;
    }
}

if(current_attack == AttackController.attack_zato_drill)
{
    if(image_index == 1)
    {
        drillx = Player.x;
        drilly = Player.y;
    }
    if(image_index == time_to_attack)
    {
        driller = instance_create(drillx, drilly, obj_zato_driller);
        driller.damage = ds_list_find_value(current_attack, 1);
        driller.image_xscale = image_xscale;
        driller.image_speed = image_speed;
        with(driller)
        {
            while(!place_meeting(x,y,Par_Tile))
            {
                y++;
            }
            y--;
        }
        attack_cooldown_curr += 4*room_speed;
        attacked = true;
    }
}

if(current_attack = AttackController.back_dash)
{
    sprite_index = walking_animation;
    hsp = image_xscale*20;
    sliding = true;
    slide_timer = slide_time_max;
    AI_mode = 1;
}
