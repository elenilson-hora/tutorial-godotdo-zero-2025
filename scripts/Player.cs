using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float Speed = 80.0f;
	private const float JumpVelocity = -400.0f;
	[Export] private AnimatedSprite2D _anim;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		Vector2 direction = Input.GetVector("left", "right","ui_up","ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (IsOnFloor())
		{
			if (direction.X > 0f)
			{
				_anim.FlipH = false;
				_anim.Play("walk");
			}
			else if (direction.X < 0f)
			{
				_anim.FlipH = true;
				_anim.Play("walk");
			}
			else
			{
				_anim.Play("idle");
			}
		}
		else
		{
			_anim.Play("jump");
		}

		

		Velocity = velocity;
		MoveAndSlide();
	}
}
