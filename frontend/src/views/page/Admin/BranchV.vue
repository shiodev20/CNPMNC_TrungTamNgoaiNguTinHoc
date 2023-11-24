<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
    <basedetail v-if="this.$route.query.action == 'detail'"></basedetail>
  </div>
</template>
<script>
import add from "@/components/branch/Add_branch.vue";
import list from "@/components/branch/List_Branch.vue";
import detail from "@/components/exambatch/Detail_Exambatch.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
    basedetail: detail,
  },
};
</script>
<style scoped></style>
