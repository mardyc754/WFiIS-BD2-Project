USE Project
GO

TRUNCATE TABLE dbo.ProjectXML;
GO

INSERT INTO dbo.ProjectXML(menuXML) values (
'<Menu>
  <Category id="1" name="Zupy">
    <Product id="1" vegetarian="1">
      <Name>Zupa pomidorowa</Name>
      <Prices>
        <Price type="medium">10.0000</Price>
      </Prices>
    </Product>
    <Product id="2" vegetarian="1">
      <Name>Zupa pieczarkowa</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="3" vegetarian="1">
      <Name>Zupa ogórkowa</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="4" vegetarian="0">
      <Name>Rosół z makaronem</Name>
      <Prices>
        <Price type="medium">14.0000</Price>
      </Prices>
    </Product>
    <Product id="5" vegetarian="0">
      <Name>Żurek</Name>
      <Prices>
        <Price type="medium">15.0000</Price>
      </Prices>
    </Product>
    <Product id="6" vegetarian="1">
      <Name>Barszcz czerwony</Name>
      <Prices>
        <Price type="medium">15.0000</Price>
      </Prices>
    </Product>
    <Product id="7" vegetarian="0">
      <Name>Krupnik</Name>
      <Prices>
        <Price type="medium">14.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="2" name="Pierogi">
    <Product id="8" vegetarian="1">
      <Name>Pierogi z serem</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="9" vegetarian="1">
      <Name>Pierogi ruskie</Name>
      <Prices>
        <Price type="medium">10.0000</Price>
      </Prices>
    </Product>
    <Product id="10" vegetarian="0">
      <Name>Pierogi z mięsem</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="11" vegetarian="1">
      <Name>Pierogi ze szpinakiem</Name>
      <Prices>
        <Price type="medium">13.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="3" name="Naleśniki">
    <Product id="12" vegetarian="1">
      <Name>Naleśniki z serem</Name>
      <Prices>
        <Price type="medium">15.0000</Price>
      </Prices>
    </Product>
    <Product id="13" vegetarian="1">
      <Name>Naleśniki z dżemem</Name>
      <Prices>
        <Price type="medium">14.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="4" name="Dania mięsne">
    <Product id="14" vegetarian="0">
      <Name>Kotlet schabowy</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="15" vegetarian="0">
      <Name>Kotlet z drobiu</Name>
      <Prices>
        <Price type="medium">12.0000</Price>
      </Prices>
    </Product>
    <Product id="16" vegetarian="0">
      <Name>Gulasz wieprzowy</Name>
      <Prices>
        <Price type="medium">14.0000</Price>
      </Prices>
    </Product>
    <Product id="17" vegetarian="0">
      <Name>Gulasz drobiowy</Name>
      <Prices>
        <Price type="medium">14.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="5" name="Napoje ciepłe">
    <Product id="18">
      <Name>Kawa</Name>
      <Prices>
        <Price type="small">5.0000</Price>
        <Price type="medium">7.5000</Price>
        <Price type="large">10.0000</Price>
      </Prices>
    </Product>
    <Product id="19">
      <Name>Herbata</Name>
      <Prices>
        <Price type="small">5.0000</Price>
        <Price type="medium">7.5000</Price>
        <Price type="large">10.0000</Price>
      </Prices>
    </Product>
    <Product id="20">
      <Name>Kakako</Name>
      <Prices>
        <Price type="small">6.0000</Price>
        <Price type="medium">9.0000</Price>
        <Price type="large">12.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="6" name="Napoje zimne">
    <Product id="21">
      <Name>Cola</Name>
      <Prices>
        <Price type="small">6.0000</Price>
        <Price type="medium">9.0000</Price>
        <Price type="large">12.0000</Price>
      </Prices>
    </Product>
    <Product id="22">
      <Name>Oranżada</Name>
      <Prices>
        <Price type="small">6.0000</Price>
        <Price type="medium">9.0000</Price>
        <Price type="large">12.0000</Price>
      </Prices>
    </Product>
    <Product id="23">
      <Name>Shake</Name>
      <Prices>
        <Price type="small">10.0000</Price>
        <Price type="medium">15.0000</Price>
        <Price type="large">20.0000</Price>
      </Prices>
    </Product>
    <Product id="24">
      <Name>Kompot</Name>
      <Prices>
        <Price type="small">4.0000</Price>
        <Price type="medium">6.0000</Price>
        <Price type="large">8.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="7" name="Pizza">
    <Product id="25" vegetarian="1">
      <Name>Margherita</Name>
      <Prices>
        <Price type="small">18.0000</Price>
        <Price type="medium">25.0000</Price>
        <Price type="large">28.0000</Price>
      </Prices>
    </Product>
    <Product id="26" vegetarian="0">
      <Name>Capricciosa</Name>
      <Prices>
        <Price type="small">20.0000</Price>
        <Price type="medium">26.0000</Price>
        <Price type="large">32.0000</Price>
      </Prices>
    </Product>
    <Product id="27" vegetarian="0">
      <Name>Pepperoni</Name>
      <Prices>
        <Price type="small">21.0000</Price>
        <Price type="medium">27.0000</Price>
        <Price type="large">33.0000</Price>
      </Prices>
    </Product>
    <Product id="28" vegetarian="0">
      <Name>Salami</Name>
      <Prices>
        <Price type="small">21.0000</Price>
        <Price type="medium">27.0000</Price>
        <Price type="large">33.0000</Price>
      </Prices>
    </Product>
    <Product id="29" vegetarian="0">
      <Name>Hawajska</Name>
      <Prices>
        <Price type="small">23.0000</Price>
        <Price type="medium">30.0000</Price>
        <Price type="large">37.0000</Price>
      </Prices>
    </Product>
    <Product id="30" vegetarian="1">
      <Name>Vegetariana</Name>
      <Prices>
        <Price type="small">25.0000</Price>
        <Price type="medium">33.0000</Price>
        <Price type="large">41.0000</Price>
      </Prices>
    </Product>
    <Product id="31" vegetarian="1">
      <Name>4 sery</Name>
      <Prices>
        <Price type="small">23.0000</Price>
        <Price type="medium">33.0000</Price>
        <Price type="large">41.0000</Price>
      </Prices>
    </Product>
  </Category>
  <Category id="8" name="Makaron">
    <Product id="32" vegetarian="0">
      <Name>Spaghetti bolognese</Name>
      <Prices>
        <Price type="medium">22.0000</Price>
      </Prices>
    </Product>
    <Product id="33" vegetarian="0">
      <Name>Spaghetti carbonara</Name>
      <Prices>
        <Price type="medium">23.0000</Price>
      </Prices>
    </Product>
  </Category>
</Menu>');