class Wombat{
	public int Health;
	public int Armor;

	public void TakeDamage(int damage){
		Health -= damage - armor;
		if(Health <= 0){
			Console.WriteLine("Я умер");
		}
	}
}

class Human{
	public int Health;
	public int Agility;

	public void TakeDamage(int damage){
		Health -= damage / Agility;
		if(Health <= 0){
			Console.WriteLine("Я умер");
		}
	}
}