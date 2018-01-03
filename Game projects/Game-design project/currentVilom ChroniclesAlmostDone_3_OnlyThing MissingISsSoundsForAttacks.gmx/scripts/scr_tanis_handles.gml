///all player handles
//controls
Key_Jump = keyboard_check_pressed(ord('W'));
if(blocking || crouching){ Key_Left = false } else { Key_Left = keyboard_check_direct(ord('A'))}
if(blocking || crouching){ Key_Right = false }else { Key_Right =  keyboard_check_direct(ord('D')) }
Key_Up = keyboard_check_direct(ord('W'));
Key_Up_Release = keyboard_check_released(ord('W'));
Key_Down = keyboard_check_direct(ord('S'));
Key_Down_Release = keyboard_check_released(ord('S'));
//Key_Shoot = keyboard_check_direct(ord('D'));
//Key_Shoot_Release = keyboard_check_released(ord('D'));
Key_Interact = keyboard_check_pressed(ord('F'));
Key_Run = keyboard_check_direct(vk_shift);
Key_Block = keyboard_check_direct(ord('L'));
if(!melee) {Key_Melee = keyboard_check_direct(ord('J'))}else {Key_Melee = false}

//Stats
scr_stamina_base();
CurAttack = baseAttack;
CurDefense = baseDefense;
CurMagic = baseMagic;
CurMR = baseMR;
CurSpeed = baseSpeed;
MaxHP = baseHP;
CurHP = MaxHP - lostHP;
SpeedMulti = 1 + running / 2
if(onGround){
    hsp = 0;
}
if(crouching && place_meeting(x,y-35,Par_Walls)){
    crouching = true
} else {
    crouching = false;
}
blocking = false;

/*//damage calc
if (hit = 1){
    lostHP = lostHP + Par_Enemy_Bullet.BulletDamage
}
//reset hit to 0
hit = 0;*/

//speed changes from stat
if (swimming){
    CurSpeed = CurSpeed / 3;
} else { //defualt
CurSpeed = CurSpeed;
}
// movementspeed calc
Speed = CurSpeed * SpeedMulti


//facing left and right & movment left and right
if (Key_Left){
    facing_Left = 1;
    facing_Right = 0;
    hsp = -Speed;
}

if (Key_Right){
    facing_Left = 0;
    facing_Right = 1;
    hsp = Speed;
}

if (Key_Left && Key_Right){
    facing_Left = 0;
    facing_Right = 1;
}
if(Key_Run){
    if(scr_stamina_cost(2)){
        running = 1;
    } else {
        running = 0;
    }
}else{
    running = 0;
}

//facing down and up and fall faster also climbing ladders

if (Key_Up){
    facing_Up = 1;
}else{
    facing_Up = 0;
}

if(Key_Down){
    facing_Down = 1;
    crouching = true;
}else{
    facing_Down = 0;
}
if(Key_Up && Key_Down){
    facing_Down = 0;
    facing_Up = 0;
}
if(facing_Down && onGround = 0 && ladder = 0){
    vsp = vsp + 0.5;
}

//jumping
if(!Key_Down = 1){
    if (Key_Jump){
        if(!swimming){
            if(onGround){
                vsp = -18;
            }else{
              if (doublejump){
                if(scr_stamina_cost(25)){
                        vsp = -15;
                        doublejump = 0;
                    }
                }
            }
        }else{
        vsp = -8
        }
    }
}
//ladder
if((facing_Up || facing_Down)&& place_meeting(x,y,Par_Ladder)){
    if(facing_Up){
        ladder = 1;
        vsp = -5;
        doublejump = 1;
    }else{
        ladder = 1;
        vsp = 5 - grav;
        doublejump = 1;
    }
}else{
ladder = 0;
}
//interaction with objects
if (Key_Interact){
    if(instance_exists(_NPC)) {
        inst = instance_nearest(x, y, _NPC);
        if(distance_to_object(inst) < 16) { //If close enough to an NPC
            //Show their messages
            with(inst) {
                if(array_length_2d(message, 0) < 5) {
                    //Manually if the dialog is entered manually
                    var i;
                    for(i = firstMessage; i < messageCount + firstMessage; i++) {
                        dialog_msg(message[i, 0], message[i, 1], message[i, 2], message[i, 3]);
                    }
                } else {
                    //From array if there is a fifth element
                    dialog_from_array(firstMessage, firstMessage + messageCount - 1);
                }
                if(firstMessage + messageCount < array_height_2d(message)) {
                    firstMessage += messageCount; 
                }
            }
        }
    }
}

//shooting

/*if (Key_Shoot){
    Charge += 1;
}
if (Key_Shoot_Release){
    if(facing_Down || facing_Up || facing_Left || facing_Right){
        shooting = true;
        instance_create(self.x,self.y,Obj_Bullet_Player);
        Charge = 0;
    }
}*/

//Melee
if(Key_Melee){
    if(!onGround){
        if(scr_stamina_cost(60)){
            imageIndex = 0;
            melee = true;
        }
    }else if(crouching){
        if(scr_stamina_cost(45)){
            imageIndex = 0;
            melee = true;
        }
    }else{//standing melee
        if(scr_stamina_cost(55)){
            imageIndex = 0;
            melee = true;
        }
    }
}

if(Key_Block && !melee && onGround){
    if(scr_stamina_cost(1)){
        blocking = true;
    }
}

//gravity
if(place_meeting(x,y,Par_Water)){
    swimming = 1;
    vsp += grav/3;
    doublejump = 1;
}else{
    vsp += grav;
    swimming = 0;
}
if(crouching){
    sprite_index = spr_tanis_collision_crouching;
} else {
    sprite_index = spr_tanis_collision;
}
// collision and movement
if (instance_place(x,y+vsp,Par_Jumpthrough) && sign(vsp) == 1){
    if (!place_meeting(x,y,Par_Jumpthrough)){
        while (!place_meeting(x,y+sign(vsp),Par_Jumpthrough)) y+=1;
        vsp = 0;
        onGround = 1;
        doublejump = 1;
        if(Key_Down = 1 and Key_Jump = 1){
        y+=grav;
        }
    }    
} 

repeat(round(abs(hsp))) {
    var blk, mov;
    mov = 0;
    blk = place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90)),y+lengthdir_y(1,grv_dir+(sign(hsp)*90)),Par_Walls);
    if(blk==1) {
        for(a=1;a<=max_slp;a+=1) {
            if(place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90))-lengthdir_x(a,grv_dir),y+lengthdir_y(1,grv_dir+(sign(hsp)*90))-lengthdir_y(a,grv_dir),Par_Walls)==0) {
                x  += lengthdir_x(1,grv_dir+(sign(hsp)*90))-lengthdir_x(a,grv_dir);
                y  += lengthdir_y(1,grv_dir+(sign(hsp)*90))-lengthdir_y(a,grv_dir);
                mov = 1;
                break;
            }
        }
        if(mov==0) {
            hsp = 0;
            break;
        }
    } else {
        if(mov==0) {
            for(a=max_slp;a>=1;a-=1) {
                if(place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90))+lengthdir_x(a,grv_dir),y+lengthdir_y(1,grv_dir+(sign(hsp)*90))+lengthdir_y(a,grv_dir),Par_Walls)==0 && (place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90))+lengthdir_x(a,grv_dir),y+lengthdir_y(1,grv_dir+(sign(hsp)*90))+lengthdir_y(a,grv_dir),Par_Jumpthrough)==0 && sign(vsp)>-1)) {
                    if(place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90))+lengthdir_x(a+1,grv_dir),y+lengthdir_y(1,grv_dir+(sign(hsp)*90))+lengthdir_y(a+1,grv_dir),Par_Walls)==1 || place_meeting(x+lengthdir_x(1,grv_dir+(sign(hsp)*90))+lengthdir_x(a+1,grv_dir),y+lengthdir_y(1,grv_dir+(sign(hsp)*90))+lengthdir_y(a+1,grv_dir),Par_Jumpthrough)==1) {
                        x  += lengthdir_x(1,grv_dir+(sign(hsp)*90))+lengthdir_x(a,grv_dir);
                        y  += lengthdir_y(1,grv_dir+(sign(hsp)*90))+lengthdir_y(a,grv_dir);
                        mov = 1;
                        break;
                    }
                }
            }
        }
        if(mov==0) {
            x+= lengthdir_x(1,grv_dir+(sign(hsp)*90));
            y+= lengthdir_y(1,grv_dir+(sign(hsp)*90));
        }
    }
}
var stop;
stop = 0;
repeat(round(abs(vsp))) {
    if(place_meeting(x+lengthdir_x(1,grv_dir-90+(sign(vsp)*90)),y+lengthdir_y(1,grv_dir-90+(sign(vsp)*90)),Par_Walls)==1) {
            if (place_meeting(x,y+1,Par_Walls)){
            vsp    = 0;
            onGround = 1;
            doublejump = 1;
            break;
        }else{
            vsp = 0;
            break;
        }
    }else{
    onGround = 0;
    }
    if(vsp==0)break;
    x+= lengthdir_x(1,grv_dir-90+(sign(vsp)*90));
    y+= lengthdir_y(1,grv_dir-90+(sign(vsp)*90));
}

free = 1;
if(place_meeting(x+lengthdir_x(1,grv_dir),y+lengthdir_y(1,grv_dir),Par_Walls)==1) {
    free = 0;
}
prevhsp = hsp;
