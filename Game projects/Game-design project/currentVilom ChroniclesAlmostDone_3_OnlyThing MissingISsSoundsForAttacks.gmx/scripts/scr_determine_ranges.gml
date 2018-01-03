///determine ranges
pref_min_range = 10000;
max_range = 0;
for(i = 0; i<ds_list_size(attacks);i++)
{
    attack = ds_list_create();
    attack = ds_list_find_value(attacks, i);
    if(pref_min_range > ds_list_find_value(attack, 5))
        pref_min_range = ds_list_find_value(attack, 5);
    if(max_range < ds_list_find_value(attack, 0))
        max_range = ds_list_find_value(attack, 0);
}
