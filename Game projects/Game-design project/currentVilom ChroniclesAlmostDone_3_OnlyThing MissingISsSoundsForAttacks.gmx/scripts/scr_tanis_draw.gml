imageIndex++
if(facing_Right){
image_xscale = 1;
} else {
image_xscale = -1;
}

if (hsp = 0 && onGround && !crouching && !melee && !blocking){
    draw_sprite_ext(spr_tanis_idle, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 20) imageIndex = -1;
} else if (onGround && !crouching && melee){
    draw_sprite_ext(spr_tanis_attacking, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    if (imageIndex == 8){
        bert = instance_create(x+154*image_xscale,y-32,obj_tanis_attacking);
        with (bert){
            greg = instance_place(x,y,Par_Enemy);
            if(greg){
                with (greg){
                lostHP = lostHP + 50 * Player.damageMult;
                if(AI_mode = 0) AI_mode = 1;
                }
            }
        instance_destroy();
        }    
    }
    if(imageIndex == 12){ 
        melee = false; 
        imageIndex = -1
    }
} else if (onGround && !crouching && blocking){
    if(imageIndex > 8){ imageIndex = 0;}
    draw_sprite_ext(spr_tanis_blocking, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);  
    if(imageIndex == 7){
    imageIndex = 4;
    }
} else if (hsp != 0 && onGround && !crouching){
    draw_sprite_ext(spr_tanis_walking, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 14) imageIndex = -1;
} else if(onGround && crouching && blocking){
    if(imageIndex > 8){ imageIndex = 0;}
    draw_sprite_ext(spr_tanis_crouching_block, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 7){
    imageIndex = 4;
    }
} else if(onGround && crouching && melee){
    draw_sprite_ext(spr_tanis_crouching_attacking, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    if (imageIndex == 8){
        bert = instance_create(x+124*image_xscale,y+29,obj_tanis_attacking);
        with (bert){
            image_angle = 270 + Player.image_xscale * 75;
            greg = instance_place(x,y,Par_Enemy);
            if(greg){
                with (greg){
                lostHP = lostHP + 35 * Player.damageMult;
                if(AI_mode = 0) AI_mode = 1;
                }
            }
        instance_destroy();
        }    
    }
    if(imageIndex == 13){
        imageIndex = -1;
        melee = false;
    }
} else if(onGround && crouching){
    draw_sprite_ext(spr_tanis_crouching, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 20) imageIndex = -1;
} else if (!onGround && melee){
    draw_sprite_ext(spr_tanis_jumping_attacking, imageIndex, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex > 6){ melee = false}
    if (imageIndex == 3){
        bert = instance_create(x+154*image_xscale,y+64,obj_tanis_attacking);
        with (bert){
        image_angle = 270 + Player.image_xscale * 65;
            greg = instance_place(x,y,Par_Enemy);
            if(greg){
                with (greg){
                lostHP = lostHP + 40 * Player.damageMult;
                if(AI_mode = 0) AI_mode = 1;
                }
            }
        instance_destroy();
        }    
    }
    if(imageIndex == 4){ 
        imageIndex = 3; 
        attackdone++;
        if(attackdone == 5){
            melee = false; 
            attackdone = 0;
        }
    }
} else if(!onGround){
    draw_sprite_ext(spr_tanis_jumping, 9, x, y, -image_xscale, image_yscale, 0, c_white, 1);
    imageIndex = 0;
} 
