using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUnit
{
    bool ShieldExists { get; set; }

    void AddHP(float hp);
    void TakeDamage(float damage);
    void SpeedChange(float speed);
    void Respawn();
}