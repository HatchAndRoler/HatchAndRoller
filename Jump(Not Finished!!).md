HatchAndRoller
==============

XNA Game

private void Jump()
        {
            player.position+=velocity;
            if (keybState.IsKeyDown(Keys.Space) && player.hasJumped == false)
            {
                player.position.Y -= 10f;
                velocity.Y = -10f;
                player.hasJumped = true;
            }
            if (player.hasJumped == true)
            {
                //int i = 1;
                velocity.Y += 0.3f * 1;
            }

            if (player.position.Y >= 700)
                player.hasJumped = false;

            if (player.hasJumped == false)
                velocity.Y = 0;
            //ADD DOUBLE JUMP!!!!

        }
