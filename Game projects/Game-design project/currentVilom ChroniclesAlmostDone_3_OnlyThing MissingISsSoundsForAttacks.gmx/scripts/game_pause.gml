///game_pause() 
/*
Takes an image of the screen, then deactivates all instances,
except controllers. The image must then be drawn, most likely
by a controller.
*/
if(!sprite_exists(Controller.sPause)) {
    //Create an image from the screen
    Controller.sPause = sprite_from_screen(view_xview[0], view_yview[0], window_get_width(), window_get_height(), false, false, 0, 0);
    //Deactivate all instances to prevent them from moving around
    instance_deactivate_all(true);
    //If you have any controllers that shouldn't be deactivated, reactivate them here
    instance_activate_object(DialogController);
    instance_activate_object(Controller);
}
