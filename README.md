# 如何使用
1、只需要2个GameObject，Joystick通常为背景，Control通常为前景；  
![Joystick1](https://raw.githubusercontent.com/RickJiangShu/Joystick-Example/master/Poster/Joystick1.jpg "Joystick1")

2、设置参数  
![Joystick2](https://raw.githubusercontent.com/RickJiangShu/Joystick-Example/master/Poster/Joystick2.jpg "Joystick2")
Control 控制对象（前景）  
Control Radius 控制半径（像素）  
Touch Area 控制区域（w和h为1，即全屏）  
Replace Time 复位缓动时间  

3、侦听事件  
```
joystick.OnTouchMove += OnJoystickMove;

private void OnJoystickMove(JoystickData data)
{
    float mx = Mathf.Cos(data.radians) * speed * Time.deltaTime * data.power;
    float mz = Mathf.Sin(data.radians) * speed * Time.deltaTime * data.power;
    target.transform.Translate(mx, 0, mz);
}
```  
  
[Example](https://github.com/RickJiangShu/Joystick-Example "Example")
  
# 演示GIF
![Joystick3](https://raw.githubusercontent.com/RickJiangShu/Joystick-Example/master/Poster/Joystick-example.gif "Joystick3")
