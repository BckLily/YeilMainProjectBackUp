#define _CRT_SECURE_NO_WARNINGS
#include<iostream>
#include<stdio.h>
#include "MyStack.h"

MyStack::MyStack()
{
	while (1)
	{
		printf("1)push 2)pop 3)all data 4) exit\n");
		int i;
		scanf("%d", &i);
		switch (i)
		{
		case 1:
			MyStack::push();
			break;
		case 2:
			MyStack::pop();
			break;
		case 3:
			MyStack::alldata();
			break;
		case 4:
			MyStack::exit();
			break;
		default:
			printf("Wrong INPUT\n\n");
			break;

		}
	}
}

void MyStack::push()
{
	// arrytop ���� ���� ������ �����Ͱ� ���ִ�
	// index��ȣ _ �����ϴ� ��ȣ�� �ƴҶ� ����
	if (arrytop < 9)
	{
		printf("���ڸ� �Է��ϼ���\n\n");
		int a;  // �Է� ���� ���� ����
		scanf("%d", &a);  // �Է��� ���� �Է�
		printf("�Է��� �� = %d\n\n", a); // ���

		arrytop++; // �迭 �ڸ��� ���� ������ �̵�
		MyData[arrytop] = a; // arrytop : ������� ������ �迭 �ڸ� �ʱⰪ
		                     // �迭�� �Է��� ���� a�� �����Ѵ�.
		printf("arry[%d] = %d\n\n", arrytop, a); // �迭 �ڸ��� ����� ���� ���
	}
	else
	{
		printf("Not Enough ���� �ڸ�\n\n");
	}

}

void MyStack::pop()
{
	if (arrytop >= 0) // �迭�� �����Ͱ� ��� 1�� �̻��� �� �մ��� �Ǵ�
	{
		printf("%d �� �ִ� %d �� pop �մϴ�\n\n", arrytop, MyData[arrytop]);
		arrytop--;
		//������ �ִ� ������ �����͸� pop �߱⿡
		//���� �ֱٿ� �Էµ� �����ʹ� arrytop 
	}
	else
	{
		printf("No Data\n\n");
	}

}

void MyStack::alldata()
{
	if (arrytop >= 0) 
	{
		for (int i = 0; i <= arrytop; i++)
		{
			printf("arry[%d] = %d\n",i, MyData[i]);
		}
		//�迭�� 0������ �����Ͱ� ����Ǳ� ������
		//0�� ���� ����ϸ�
		//������ �����Ͱ� ����� ��ġ�� arrytop ��ġ�̱� ������
		//�迭 �������� �ƴ϶� arrytop�� ������ ����ؾ��Ѵ�.
	}
	else
	{
		printf("No Data\n\n");
	}
}

void MyStack::exit()
{
	return;
}
