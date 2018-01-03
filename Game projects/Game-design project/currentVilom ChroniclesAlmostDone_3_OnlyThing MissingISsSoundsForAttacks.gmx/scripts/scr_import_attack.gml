///Arguments: Name in attack controller, name in enemy.
attack_name = argument0;
attack_Name = argument1;
attack_Name[0] = AttackController.attack_name[0];//range
attack_Name[1] = AttackController.attack_name[1];//damage
attack_Name[2] = AttackController.attack_name[2];//effectrange
attack_Name[3] = AttackController.attack_name[3];//is countermove
attack_Name[4] = AttackController.attack_name[4];//animation
attack_Name[5] = AttackController.attack_name[5];//preferable min range
attack_Name[6] = AttackController.attack_name[6];//projectile
ds_list_add(attacks, attack_Name);
if(max_range<AttackController.attack_name[0])
    max_range = AttackController.attack_name[0];
if(pref_min_range>AttackController.attack_name[5])
    pref_min_range = AttackController.attack_name[5];

