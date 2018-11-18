using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface ICardStackUIComponent : ICustomUIComponent
{
    ICardStack GetCardStack();

    void SetCardStack(ICardStack stack);


}
