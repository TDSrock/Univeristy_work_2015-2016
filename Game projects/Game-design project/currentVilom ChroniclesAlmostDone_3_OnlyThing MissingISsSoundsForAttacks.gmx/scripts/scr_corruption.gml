///Arguments: Range, Corruption at epicenter, Corruption at edge, Corruption max, Location_X , Location_Y, Time.
var Range = argument0;
var Coruption_epicenter = argument1;
var Corruption_edge = argument2;
var Corruption_max = argument3;
var X_org = argument4;
var Y_org = argument5;
var Time = argument6;

var l,t;
l=ds_list_create();
while 1{
    t=collision_circle(X_org,Y_org,Range,Par_Tile,false,true);
    if t{
        ds_list_add(l,t);
        instance_deactivate_object(t);
    } else {
    break;
    }
}
Epicenter = instance_create(X_org, Y_org, Corruption_scr_epicenter_obj);
for(t=0;t<ds_list_size(l);t+=1){
    instance_operating = ds_list_find_value(l,t);
    instance_activate_object(instance_operating);
    Epicenter.instance_operating = instance_operating;
    with(Epicenter){
        distance = distance_to_object(instance_operating);
    }
    LocationMultip = 1 - Epicenter.distance/Range;
    Current_corruption = instance_operating.corruption;

    Projected_corruption = instance_operating.corruption + Corruption_edge + (Corruption_max - Corruption_edge) * LocationMultip;
    if(Projected_corruption != 0){
    diminshingReturns = 1 - instance_operating.corruption / Projected_corruption/2;
    } else {
    diminshingReturns = 1
    }
    Projected_corruption *= diminshingReturns;
    if(Projected_corruption > Corruption_max)
        Projected_corruption = Corruption_max;
    if(Projected_corruption > Current_corruption){
        instance_operating.corruptionGoal = Projected_corruption;
        instance_operating.corruptionPerStep = (Projected_corruption - Current_corruption) / (Time * room_speed)
        instance_operating.corruptionCompleteSteps = Time * room_speed;
    }
}
ds_list_destroy(l);
with(Epicenter){
    instance_destroy();
}
