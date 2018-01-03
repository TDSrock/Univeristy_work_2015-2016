imageIndex++
if(facing_Right){
image_xscale = 1;
} else {
image_xscale = -1;
}
if(teleporting){
    draw_sprite_ext(spr_vairl_teleport, imageIndex,x,y,image_xscale, image_yscale, 0, c_white, 1); 
    if(imageIndex > 13){imageIndex = -1}
    if(imageIndex == 8){
    tphsp = (Key_Right - Key_Left) * 250
    tpvsp = (Key_Down - Key_Up) * 250
    tpForce = true;
    }
    
    teleported = true
    if(imageIndex == 13){
        teleporting = false;
        teleported = false;
    }
}else if(shooting){
    draw_sprite_ext(spr_vairl_fireball, imageIndex,x,y,image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex > 6){imageIndex = -1}
    if(imageIndex == 5){
    bullet = instance_create(self.x + image_xscale * 20,self.y,Obj_Bullet_Player);
    bullet.image_xscale = image_xscale;
    bullet.damage = 25 * Player.damageMult;
    shooting = false;
    }
    
}else if(hover && !melee){
    draw_sprite_ext(spr_vairl_teleport, 0 ,x, y, image_xscale, image_yscale, 0, c_white, 1)
    imageIndex = -1;
}else if (hsp = 0 && onGround && !crouching && !melee && !blocking){
    draw_sprite_ext(spr_vairl_idle, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 20) imageIndex = -1;
} else if (onGround && !crouching && melee){
    draw_sprite_ext(spr_vairl_melee, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if (imageIndex > 13){ imageIndex = -1}
    if (imageIndex == 8){
        bert = instance_create(x+100*image_xscale,y-20,obj_vairl_attacking);
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
    draw_sprite_ext(spr_vairl_blocking, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);  
    if(imageIndex == 1){
    imageIndex = -1;
    }
} else if (hsp != 0 && onGround && !crouching){
    draw_sprite_ext(spr_vairl_walking, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 20) imageIndex = -1;
} else if(onGround && crouching && blocking){
    if(imageIndex > 13){ imageIndex = 0;}
    draw_sprite_ext(spr_vairl_crouching_block, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 10){
    imageIndex = 8;
    }
} else if(onGround && crouching && melee){
    draw_sprite_ext(spr_vairl_crouching_melee, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex > 14){ imageIndex = -1}
    if (imageIndex == 11){
        bert = instance_create(x+74*image_xscale,y+ 34,obj_vairl_attacking);
        with (bert){
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
    if(imageIndex == 14){
        imageIndex = -1;
        melee = false;
    }
} else if(onGround && crouching){
    draw_sprite_ext(spr_vairl_crouching_block, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex == 1){ imageIndex = -1 }else{ imageIndex = 0;}
} else if (!onGround && melee){
    draw_sprite_ext(spr_vairl_jumping_melee, imageIndex, x, y, image_xscale, image_yscale, 0, c_white, 1);
    if(imageIndex > 16){ ImageIndex = 10}
    if(imageIndex < 10){ imageIndex = 10}
    if (imageIndex == 16){
        bert = instance_create(x+54*image_xscale,y,obj_vairl_attacking);
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
    if(imageIndex == 16){ 
        imageIndex = -1; 
        melee = false
    }
} else if(!onGround){
    draw_sprite_ext(spr_vairl_jump, 9, x, y, image_xscale, image_yscale, 0, c_white, 1);
    imageIndex = -1;
} 
