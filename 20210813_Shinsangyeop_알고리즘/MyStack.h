#pragma once
class MyStack
{
    // 자료구조

/*
  스택(stack)
    FILO(First In Last Out)
    - 먼저 들어간 데이터가 나중에 나온다.
    - 컵으로 비유한다.
    - 대표적으로 CLtr Z
    - 데이터 삽입 PUSH
    - 데이터 추출 POP
*/
public:
	MyStack();

	int MyData[10]; //데이터를 저장할 배열


    void push();
    void pop();
    void alldata();
    void exit();
    int arrytop = -1;
};

