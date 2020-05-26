using System;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var livingBeings = new List<LivingСreature>()
            {
                new Human(1000, 10),
                new Wombat(),
            };

            while (livingBeings.Where(x => x.IsAlive).Count() > 0)
            {
                int damage;
                if (int.TryParse(Console.ReadLine(), out damage))
                    livingBeings.ForEach(livingBeing =>
                    {
                        livingBeing.TakeDamage(damage);
                        Console.WriteLine("-------------------------------");
                    });
                else
                    continue;

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    internal abstract class LivingСreature
    {
        protected const uint DefaultValueHealth = 100;

        public int Health => _health;
        public bool IsAlive => _health > 0;

        private int _health;

        protected LivingСreature(uint health)
        {
            this._health = (int)health.CheckAtZero();
        }

        internal virtual void TakeDamage(int damage)
        {
            damage.CheckAtNegative();

            Console.WriteLine(string.Concat(GetType().Name, " try damaged at: ", damage));

            if (!IsAlive || damage == 0)
                return;

            _health -= Math.Min(damage, _health);

            if (_health == 0)
                Console.WriteLine(string.Concat(GetType().Name, " : I died"));
            else
                Console.WriteLine(string.Concat(GetType().Name, " : Health = ", Health));
        }
    }

    internal abstract class DefendingСreature<T> : LivingСreature where T : class
    {
        private const uint DefaultValueDefendingAbility = 10;

        protected readonly int valueDefend;

        protected DefendingСreature(uint health, uint valueDefend) : base(health)
        {
            this.valueDefend = (int)valueDefend;
        }

        protected DefendingСreature() : this(DefaultValueHealth, DefaultValueDefendingAbility)
        {
        }

        internal override sealed void TakeDamage(int damage)
        {
            base.TakeDamage(GetModifyDamage(damage.CheckAtNegative(), valueDefend));
        }

        protected abstract int GetModifyDamage(int damage, int valueDefend);
    }

    internal class Wombat : DefendingСreature<Wombat>
    {
        protected override int GetModifyDamage(int damage, int valueDefend)
        {
            return damage - Math.Min(damage, valueDefend);
        }
    }

    internal class Human : DefendingСreature<Human>
    {
        public Human(uint health, uint valueDefend) : base(health, valueDefend.CheckAtZero())
        {
        }

        protected override int GetModifyDamage(int damage, int valueDefend)
        {
            return (int)Math.Round((float)damage / valueDefend, MidpointRounding.AwayFromZero);
        }
    }

    internal static class UintIntExtensions
    {
        internal static uint CheckAtZero(this uint value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException("Value should not be zero");

            return value;
        }

        internal static int CheckAtNegative(this int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Value should not be negative");

            return value;
        }
    }
}
