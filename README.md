# ПСНР-5К

## Диапазоны переключателей

- **Ширина** -
- **←→** -
- **Яркость** - [0; 1] - нормированное значение.
- **Фокус** - [0; 1] - нормированное значение.
- **Видео А** - [0; 1] - нормированное значение.
- **Азимут** - [0; 81] -Азимут  имеет разрыв значений [19.5; 60)+[0; 40.5].
- **Дальность** - [50; 9980] - не до 15000, потому что так работает блок.
- **УПЧ** - [0; 1] - нормированное значение.
- **Громкость** - [0; 1], потому что _AudioSource.volume_ принимает значения от 0 до 1.
- **Н**
- **\_П\_** -
- **-о-** -
- **Отражатель**

## К сведению

### Генерация правой линии (NoiseLine)

Правая линия состоит и трёх слоёв: **base**, **splash**, **noise**.  
В корутине `NoiseCoroutine` каждые `_delay` секунд происходит генерация шума.  
Шум генерируется _noiseStrategy, который меняется при нажатии на СДЦ/Штатный.  
Всплеск обновляется только при повороте приёмопередатчика.
Базовый слой не меняется никогда и служит как основа, чтобы можно было независимо регулировать силу шумов и всплесков.
