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
