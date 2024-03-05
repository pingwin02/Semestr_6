#include <windows.h>
#include <stdio.h>
#include "IKlasa2.h"

int main() {
	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);

	IKlasa* klasaPtr = NULL;
	HRESULT classGetInstanceResult = CoCreateInstance(CLSID_Klasa2, NULL, CLSCTX_INPROC_SERVER, IID_IKlasa2, (void**)&klasaPtr);

	if (!FAILED(classGetInstanceResult)) {
		klasaPtr->Test("klasa stworzona poprawnie (instancja pobrana), indeks: 188597, manifest");
		klasaPtr->Release();
	}
	else {
		printf("klasa2 nie dziala (instancja nie pobrana)");
	}

	return 0;
};
