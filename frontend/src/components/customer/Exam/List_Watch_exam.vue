<template>
  <div class="parent">
    <h3 class="title">Kỳ thi hiện tại</h3>
    <div class="boss-card">
      <div
        class="card"
        :id="'item' + item.id"
        v-for="item in list_exam"
        :key="item.id"
      >
        <img
          src="https://th.bing.com/th/id/R.8ccde3d38e86d4ac781407b78cdd5a94?rik=iQprPUFe5T0K8A&riu=http%3a%2f%2fpromolocus.com%2fwp-content%2fuploads%2f2019%2f10%2fphuong-phap-hoc-truc-tuyen.png&ehk=rbIlnT%2brKSAtrZZlUIaU9dzmADnU2YWJmvMbLbsz4k0%3d&risl=&pid=ImgRaw&r=0"
          alt=""
          class="image-card"
        />
        <div class="card-content">
          <h3 class="card-item">Kỳ thi {{ item.name }}</h3>
          <p class="price">{{ format_money(item.price) }}</p>
        </div>
        <div class="setting">
          <button
            class="button btn"
            @click="
              this.$router.push({
                name: 'list-exam',
                query: { action: 'registed', id: item.id },
              })
            "
          >
            Xem ngay
          </button>
        </div>
      </div>

      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
</template>
<script>
import { computed, ref, watch } from "vue";
import store from "@/store";
export default {
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_exam) {
      const div = document.getElementById("item" + this.list_exam[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    store.dispatch("exam/get_all");
    const list_exam = computed(() => {
      return store.state.exam.exam;
    });

    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_exam.value) {
        const div = document.getElementById("item" + list_exam.value[item].id);
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (list_exam.value == null) {
        return 0;
      }
      return Math.ceil(list_exam.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
      console.log(this_page.value);
    };
    const format_money = (value) => {
      return value.toLocaleString("vi-VN", {
        currency: "VND",
        style: "currency",
      });
    };

    return {
      list_exam,
      run_app,
      count_page,
      this_page,
      limit_page,
      chage,
      format_money,
    };
  },
};
</script>
<style scoped>
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
  gap: 10px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
.title {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
  margin-bottom: 17px;
}
.setting {
  padding: 20px;
  padding-top: 0px;
  text-align: end;
}
.Header {
  color: #071952;
  text-align: right;
  font-family: Roboto;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  margin-right: 30px;
}
.content-header {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.card-item {
  color: #071952;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}

.price {
  color: rgb(185, 4, 4);
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  margin: 10px 0;
}

.btn {
  border: 2px solid #071952;
  color: #071952;
  padding: 10px 30px;
  border-radius: 10px;
  cursor: pointer;
}
.parent {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
.boss-card {
  background-color: white;
  display: grid;
  grid-template-columns: repeat(3, 300px);
  gap: 25px;
  border-radius: 10px;
  padding: 25px;
  padding-bottom: 60px;
  position: relative;
  min-height: 600px;
  min-width: 800px;
}
.image-card {
  width: 100%;
}

.card {
  position: relative;
  border-radius: 10px;
  background: #f7f7f7;
  overflow: hidden;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.card-content {
  text-align: start;
  padding: 10px;
}
h3 {
  margin: 0;
}
</style>
