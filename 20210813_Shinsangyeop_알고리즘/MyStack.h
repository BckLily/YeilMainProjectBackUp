#pragma once
class MyStack
{
    // �ڷᱸ��

/*
  ����(stack)
    FILO(First In Last Out)
    - ���� �� �����Ͱ� ���߿� ���´�.
    - ������ �����Ѵ�.
    - ��ǥ������ CLtr Z
    - ������ ���� PUSH
    - ������ ���� POP
*/
public:
	MyStack();

	int MyData[10]; //�����͸� ������ �迭


    void push();
    void pop();
    void alldata();
    void exit();
    int arrytop = -1;
};

