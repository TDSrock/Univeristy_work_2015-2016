if(timeOffCombat > staminaRegenTime){
    if(staminaRegen + stamina > maxStamina){
         stamina = maxStamina;
     } else {
        stamina += staminaRegen;
     }
}
if(timeOffCombatHealth > staminaRegenTime * 4){
     if(healthRegen + CurHP > MaxHP){
        lostHP = 0;
     } else {
        lostHP -= healthRegen;
     }
}
if(timeOffCombat < staminaRegenTime * 4+ 2){
    timeOffCombat++
    timeOffCombatHealth++
}
dispStamina = stamina / maxStamina * 100
//The following line needs to be in the objects draw event if you whish to show it's stamina bar:
//draw_healthbar(self.x - 32,self.y -80,self.x + 32,self.y - 75, dispStamina,c_black, make_color_rgb(0, 130, 50), make_color_rgb(0, 232, 50), 0,true,true);

