cost  = argument0;
if(stamina < cost){ return false; }
timeOffCombat = 0;
stamina -= cost;
return true;
