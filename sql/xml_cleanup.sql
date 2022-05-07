-- SELECT * from dbo.ProjectXML;
TRUNCATE TABLE dbo.ProjectXML;

INSERT INTO dbo.ProjectXML(menuXML) values (
'<Menu>
  <Category id="1" name="Zupy">
    <Product id="1" vegetarian="1">
      <Name>Zupa pomidorowa</Name>
      <Prices>
        <PriceMedium>10.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="2" vegetarian="1">
      <Name>Zupa pieczarkowa</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="3" vegetarian="1">
      <Name>Zupa ogórkowa</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="4" vegetarian="0">
      <Name>Rosó³ z makaronem</Name>
      <Prices>
        <PriceMedium>14.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="5" vegetarian="0">
      <Name>¯urek</Name>
      <Prices>
        <PriceMedium>15.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="6" vegetarian="1">
      <Name>Barszcz czerwony</Name>
      <Prices>
        <PriceMedium>15.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="7" vegetarian="0">
      <Name>Krupnik</Name>
      <Prices>
        <PriceMedium>14.0000</PriceMedium>
      </Prices>
    </Product>
  </Category>
  <Category id="2" name="Pierogi">
    <Product id="8" vegetarian="1">
      <Name>Pierogi z serem</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="9" vegetarian="1">
      <Name>Pierogi ruskie</Name>
      <Prices>
        <PriceMedium>10.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="10" vegetarian="0">
      <Name>Pierogi z miêsem</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="11" vegetarian="1">
      <Name>Pierogi ze szpinakiem</Name>
      <Prices>
        <PriceMedium>13.0000</PriceMedium>
      </Prices>
    </Product>
  </Category>
  <Category id="3" name="Naleœniki">
    <Product id="12" vegetarian="1">
      <Name>Naleœniki z serem</Name>
      <Prices>
        <PriceMedium>15.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="13" vegetarian="1">
      <Name>Naleœniki z d¿emem</Name>
      <Prices>
        <PriceMedium>14.0000</PriceMedium>
      </Prices>
    </Product>
  </Category>
  <Category id="4" name="Dania miêsne">
    <Product id="14" vegetarian="0">
      <Name>Kotlet schabowy</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="15" vegetarian="0">
      <Name>Kotlet z drobiu</Name>
      <Prices>
        <PriceMedium>12.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="16" vegetarian="0">
      <Name>Gulasz wieprzowy</Name>
      <Prices>
        <PriceMedium>14.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="17" vegetarian="0">
      <Name>Gulasz drobiowy</Name>
      <Prices>
        <PriceMedium>14.0000</PriceMedium>
      </Prices>
    </Product>
  </Category>
  <Category id="5" name="Napoje ciep³e">
    <Product id="18">
      <Name>Kawa</Name>
      <Prices>
        <PriceSmall>5.0000</PriceSmall>
        <PriceMedium>7.5000</PriceMedium>
        <PriceLarge>10.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="19">
      <Name>Herbata</Name>
      <Prices>
        <PriceSmall>5.0000</PriceSmall>
        <PriceMedium>7.5000</PriceMedium>
        <PriceLarge>10.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="20">
      <Name>Kakako</Name>
      <Prices>
        <PriceSmall>6.0000</PriceSmall>
        <PriceMedium>9.0000</PriceMedium>
        <PriceLarge>12.0000</PriceLarge>
      </Prices>
    </Product>
  </Category>
  <Category id="6" name="Napoje zimne">
    <Product id="21">
      <Name>Cola</Name>
      <Prices>
        <PriceSmall>6.0000</PriceSmall>
        <PriceMedium>9.0000</PriceMedium>
        <PriceLarge>12.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="22">
      <Name>Oran¿ada</Name>
      <Prices>
        <PriceSmall>6.0000</PriceSmall>
        <PriceMedium>9.0000</PriceMedium>
        <PriceLarge>12.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="23">
      <Name>Shake</Name>
      <Prices>
        <PriceSmall>10.0000</PriceSmall>
        <PriceMedium>15.0000</PriceMedium>
        <PriceLarge>20.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="24">
      <Name>Kompot</Name>
      <Prices>
        <PriceSmall>4.0000</PriceSmall>
        <PriceMedium>6.0000</PriceMedium>
        <PriceLarge>8.0000</PriceLarge>
      </Prices>
    </Product>
  </Category>
  <Category id="7" name="Pizza">
    <Product id="25" vegetarian="1">
      <Name>Margherita</Name>
      <Prices>
        <PriceSmall>18.0000</PriceSmall>
        <PriceMedium>25.0000</PriceMedium>
        <PriceLarge>28.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="26" vegetarian="0">
      <Name>Capricciosa</Name>
      <Prices>
        <PriceSmall>20.0000</PriceSmall>
        <PriceMedium>26.0000</PriceMedium>
        <PriceLarge>32.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="27" vegetarian="0">
      <Name>Pepperoni</Name>
      <Prices>
        <PriceSmall>21.0000</PriceSmall>
        <PriceMedium>27.0000</PriceMedium>
        <PriceLarge>33.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="28" vegetarian="0">
      <Name>Salami</Name>
      <Prices>
        <PriceSmall>21.0000</PriceSmall>
        <PriceMedium>27.0000</PriceMedium>
        <PriceLarge>33.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="29" vegetarian="0">
      <Name>Hawajska</Name>
      <Prices>
        <PriceSmall>23.0000</PriceSmall>
        <PriceMedium>30.0000</PriceMedium>
        <PriceLarge>37.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="30" vegetarian="1">
      <Name>Vegetariana</Name>
      <Prices>
        <PriceSmall>25.0000</PriceSmall>
        <PriceMedium>33.0000</PriceMedium>
        <PriceLarge>41.0000</PriceLarge>
      </Prices>
    </Product>
    <Product id="31" vegetarian="1">
      <Name>4 sery</Name>
      <Prices>
        <PriceSmall>23.0000</PriceSmall>
        <PriceMedium>33.0000</PriceMedium>
        <PriceLarge>41.0000</PriceLarge>
      </Prices>
    </Product>
  </Category>
  <Category id="8" name="Makaron">
    <Product id="32" vegetarian="0">
      <Name>Spaghetti bolognese</Name>
      <Prices>
        <PriceMedium>22.0000</PriceMedium>
      </Prices>
    </Product>
    <Product id="33" vegetarian="0">
      <Name>Spaghetti carbonara</Name>
      <Prices>
        <PriceMedium>23.0000</PriceMedium>
      </Prices>
    </Product>
  </Category>
</Menu>');