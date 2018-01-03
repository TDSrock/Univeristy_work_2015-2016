//Define name of script better later!!!
/*Mode list here: 
0=standby(do nothing but look around)
1=Patrolling(move from place one to place two, pathfinding required)
2=Searching(activly search for the player, only use the players last know location AND direction to make a guess of where he is)
3=Combat(attack player aim at target with highest "agro" value)
4=Chatting with other instance
5=Cutscene(no codeing, the cutscene will have triggers to move in and out of this)
*/

randomize();
if(AI_mode = 0){//patrolling
    //if(Look_Timer_Start < Look_Timer_end){
    //    Look_Timer_Start = TimeController.TrueSeconds;
     //   if (facing = facing_goal){
     //       if(Last_facing_goal > 90){
     //           while(Valid_goal=0){
     //           facing_goal = random_range(0,360);
     //               if(facing_goal <= 30 or facing_goal >=330){
     //               Valid_goal=1;
     //               }
     //           }
     //       }
     //   }
     //   Look_Timer_start = TimeController.TrueSeconds + random_range(3, 8);
    //}
    //if(Look_Timer = random_range(n1, n2));
    
    if(time_to_check == 0)
    {
        idle = !idle;
        time_to_check = floor(random_range(30, 300));
    }
    time_to_check--;
    if(idle || idle_animation == walking_animation)
    {
        hsp = 0;
        if(random(1)>0.98)
        {
            facing = (facing + 180) mod 360;
        }
        sprite_index = idle_animation;
    }
    else
    {
        if(x < origin - max_walking_distance)
        {
            facing = 0;
        }
        else if (x > origin + max_walking_distance)
        {
            facing = 180;
        }
        else if(random(1)>0.99 || place_meeting(x-1,y,Par_Tile)||place_meeting(x+1,y,Par_Tile) || place_meeting(x,y,Par_Enemy) || !place_meeting(x,y+1,Par_Tile))
        {
            facing = (facing + 180) mod 360;
            if(facing<270 && facing>90)
                hsp = -walking_speed;
            else
                hsp = walking_speed;
        }
        sprite_index = walking_animation;
    }
    playerDir = point_direction(x,y,Player.x,Player.y);//direction from guard to player
    //if player is within NPC's field of vision facing 0-360, FOV 0-180
    if ((playerDir>facing-FOV && playerDir<facing+FOV) || (360 - playerDir>facing-FOV && 360 - playerDir<facing+FOV)){
      //if player is within NPC's sight distance
      if point_distance(x,y,Player.x,Player.y)<=SIGHTMAX{
        //if there is no solid (wall) between guard and player
        if !collision_line(x,y,Player.x,Player.y,Par_Walls,false,true){
          AI_mode=1
        }
      }
    }
    if(place_meeting(x,y,Player))
        AI_mode = 1;
    
}
else if (AI_mode =1){//moving towards player to get in range.
    playerDir = point_direction(x,y,Player.x,Player.y);//direction from guard to player
    facing = playerDir;
    if(slide_timer == 0)
    {    
        if(collision_line(x,y,Player.x,Player.y,Par_Tile, false, true)||point_distance(x,y,Player.x,Player.y)>SIGHTMAX)
        {
            AI_mode = 0;
            if(facing<270 && facing>90)
              facing = 180;
            else
              facing = 0;
        }
        
        if(facing<270 && facing>90)
            hsp = -walking_speed;
        else
            hsp = walking_speed;
            
        /*if(ds_list_size(attacks)!=0)
        {
            attack = ds_list_create();
            attack = ds_list_find_value(attacks, 0);
            if(ds_list_find_value(attack,1) ==25)
                instance_destroy();
            ds_list_destroy(attack);
        }*/
        
        sprite_index = walking_animation;
        scr_determine_attacks();
    }
    else
    {
        slide_timer--;
    }
}
else if (AI_mode =2){//Attacking stance
    hsp = 0;  
    sprite_index = ds_list_find_value(current_attack, 4);
    grav = 0;
    scr_special_attacks();
    
    if(image_index == 0 && attacked)
    {
        attacked = false;
        AI_mode = 1;
        grav = 1;
    }
    if(image_index == time_to_attack || time_to_attack == -1)
    {
        if(ds_list_find_value(current_attack, 6) == obj_no_projectile)
        {   
            if(ds_list_find_value(current_attack, 10) == obj_self_collision && !self_attack)
            {
                if(place_meeting(x,y,Player)&&!Player.blocking)
                {
                    Player.lostHP += ds_list_find_value(current_attack,1);
                    Player.timeOffCombat = 0;
                    Player.timeOffCombatHealth = 0;
                    self_attack = true;
                }
            }
            else if(ds_list_find_value(current_attack, 10) != obj_no_collision)
            {
                collision_mask = instance_create(x - ds_list_find_value(current_attack, 8)*image_xscale, y + ds_list_find_value(current_attack, 9), ds_list_find_value(current_attack, 10)); 
                collision_mask.damage = ds_list_find_value(current_attack,1);
                collision_mask.image_xscale = image_xscale;
                with(collision_mask)
                {
                    if(place_meeting(x,y,Player)&&!Player.blocking)
                    {
                        Player.lostHP += damage;
                        Player.timeOffCombat = 0;
                        Player.timeOffCombatHealth = 0;
                    }
                    instance_destroy();
                }
            }
        }
        else
        {
            projectile = instance_create(x - ds_list_find_value(current_attack, 8)*image_xscale, y + ds_list_find_value(current_attack, 9),ds_list_find_value(current_attack,6));
            if(facing<270&&facing>90)
                projectile.image_xscale = -1;
            projectile.BulletDamage = ds_list_find_value(current_attack, 1);
        }
        attacked = true;
    }
}
else if (AI_mode =3){//Backing off
    playerDir = point_direction(x,y,Player.x,Player.y);//direction from guard to player
    facing = playerDir;
        
    if(collision_line(x,y,Player.x,Player.y,Par_Tile, false, true)||point_distance(x,y,Player.x,Player.y)>SIGHTMAX)
    {
        AI_mode = 0;
        if(facing<270 && facing>90)
          facing = 180;
        else
          facing = 0;
    }
    
    if(facing<270 && facing>90)
        hsp = walking_speed;
    else
        hsp = -walking_speed;
        
    /*if(ds_list_size(attacks)!=0)
    {
        attack = ds_list_create();
        attack = ds_list_find_value(attacks, 0);
        if(ds_list_find_value(attack,1) ==25)
            instance_destroy();
        ds_list_destroy(attack);
    }*/
    
    sprite_index = idle_animation;
    scr_determine_attacks();
}
